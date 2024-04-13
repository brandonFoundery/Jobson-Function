using Jobson.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Jobson_Data.Models;

public class ApplicationContext : ApiAuthorizationDbContext<ApplicationUser>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationContext(DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IHttpContextAccessor httpContextAccessor)
        : base(options, operationalStoreOptions)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public virtual DbSet<Job>? Jobs { get; set; }
    public virtual DbSet<UpworkRssFeedUrl>? UpworkRssFeedUrls { get; set; }
    public virtual DbSet<UpworkProfile>? UpworkProfiles { get; set; }
    public virtual DbSet<ProfileType>? ProfileTypes { get; set; }
    //LLMModel
    public virtual DbSet<LLMModel>? LLMModels { get; set; }
    public virtual DbSet<LLMPrompt>? LLMPrompts { get; set; }
    public DbSet<Tenant>? Tenants { get; set; }
    //TrelloBoard

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();

        modelBuilder.Entity<Job>()
            .HasOne(j => j.UpworkProfile) // Navigation property in Job
            .WithMany(p => p.Jobs) // Inverse navigation property in UpworkProfile
            .HasForeignKey(j => j.UpworkProfileId) // Foreign key in Job
            .OnDelete(DeleteBehavior.NoAction); // Handling delete behavior
    }

    // Override SaveChangesAsync
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var tenantId = GetTenantId();
        var currentUsername = GetCurrentUsername();
        var currentUserId = GetCurrentUserId();

        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            if (entry.Entity is IDomainObjectForTenant tenantEntity)
            {
                tenantEntity.TenantId = tenantId;
            }
            if (entry.Entity is IDomainObjectWithCreateAndUpdate trackable)
            {
                var timestamp = DateTime.UtcNow; // Use UTC time or your preferred time zone

                if (entry.State == EntityState.Added)
                {
                    trackable.CreatedDate = timestamp;
                    trackable.CreatedBy = currentUsername;
                    trackable.CreatedById = currentUserId;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    trackable.UpdatedDate = timestamp;
                    trackable.UpdatedBy = currentUsername;
                    trackable.UpdatedById = currentUserId;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private string GetCurrentUsername()
    {
        var usernameClaim = _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;
        return usernameClaim ?? "Anonymous"; // Or handle the default case as needed
    }

    private string GetCurrentUserId()
    {
        var usernameClaim = _httpContextAccessor.HttpContext?.User.FindFirst("id")?.Value;
        return usernameClaim ?? "Anonymous"; // Or handle the default case as needed
    }

    private int GetTenantId()
    {
        var tenantIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("TenantId")?.Value;
        return tenantIdClaim != null ? int.Parse(tenantIdClaim) : 1; // Or handle the default case as needed
    }

}

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<UpworkRssFeedUrl>().HasData(
        //    new UpworkRssFeedUrl
        //    {
        //        Id = 1,
        //        Name = "Expert Asp.Net Jobs",
        //        Url = "https://www.upwork.com/ab/feed/jobs/rss?amount=1000-4999&category2_uid=531770282580668418&contractor_tier=3&paging=0-10&q=asp.net&sort=recency&t=1&api_params=1&securityToken=a833c2c7918b7c7527f8e8deb3174ff06d3b39c6ac7621b2fd95dae3cf913e71113798900e6bfb6cd1798607d8a5c339f6bb29f2b424395cb19dbefbf4fd0041&userUid=1750194899056226304&orgUid=1750194899056226305",
        //    });
    }
}

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Configure OperationalStoreOptions using default setup or specific setup
        services.Configure<OperationalStoreOptions>(options =>
        {
            // Configure your OperationalStoreOptions here if needed
        });

        var serviceProvider = services.BuildServiceProvider();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        string connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        // Pass in OperationalStoreOptions and IHttpContextAccessor
        return new ApplicationContext(optionsBuilder.Options,
            serviceProvider.GetRequiredService<IOptions<OperationalStoreOptions>>(),
            serviceProvider.GetService<IHttpContextAccessor>());
    }
}

