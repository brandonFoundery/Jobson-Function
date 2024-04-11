using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Jobson_Data.Models;

namespace Jobson.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    public virtual DbSet<Job> Jobs { get; set; }
    public virtual DbSet<UpworkRssFeedUrl> UpworkRssFeedUrls { get; set; }
    public virtual DbSet<UpworkProfile> UpworkProfiles { get; set; }
    public virtual DbSet<ProfileType> ProfileTypes { get; set; }
    //LLMModel
    public virtual DbSet<LLMModel> LLMModels { get; set; }
    public virtual DbSet<LLMPrompt> LLMPrompts { get; set; }
    //TrelloBoard
    public virtual DbSet<TrelloBoard> TrelloBoards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }

}

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UpworkRssFeedUrl>().HasData(
            new UpworkRssFeedUrl
            {
                Id = 1,
                Name="Expert Asp.Net Jobs",
                Url = "https://www.upwork.com/ab/feed/jobs/rss?amount=1000-4999&category2_uid=531770282580668418&contractor_tier=3&paging=0-10&q=asp.net&sort=recency&t=1&api_params=1&securityToken=a833c2c7918b7c7527f8e8deb3174ff06d3b39c6ac7621b2fd95dae3cf913e71113798900e6bfb6cd1798607d8a5c339f6bb29f2b424395cb19dbefbf4fd0041&userUid=1750194899056226304&orgUid=1750194899056226305",
            });
    }
}

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        string connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationContext(optionsBuilder.Options);
    }

    public static ApplicationContext Create()
    {
        var dtfactory = new ApplicationContextFactory();
        return dtfactory.CreateDbContext(null);
    }
}