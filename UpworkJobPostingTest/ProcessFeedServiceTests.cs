using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Jobson.Models;
using Jobson.Repositories;
using Jobson.Services;
using Jobson.UpworkDTOs;

namespace UpworkJobPostingTest
{
    //Convert to NUnit test
    [TestFixture]
    public class ProcessFeedServiceTests
    {
        private Mock<ApplicationContext> _mockContext;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<IJobFilterService> _mockJobFilterService;
        private Mock<ICoverletterService> _mockCoverletterService;
        private Mock<IScraperService> _mockScraperService;
        private Mock<IUpworkProfileService> _mockUpworkProfileService;
        private Mock<ILogger<ProcessFeedService>> _mockLogger;
        private Mock<IUpworkGraphQLService> _mockUpworkGraphQLService;
        private Mock<IJobRepository> _mockJobRepository;

        public ProcessFeedServiceTests()
        {

        }
        public virtual List<string> UpworkRssFeedUrls { get; set; }

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<ApplicationContext>();
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockJobFilterService = new Mock<IJobFilterService>();
            _mockCoverletterService = new Mock<ICoverletterService>();
            _mockScraperService = new Mock<IScraperService>();
            _mockUpworkProfileService = new Mock<IUpworkProfileService>();
            _mockLogger = new Mock<ILogger<ProcessFeedService>>();
            _mockUpworkGraphQLService = new Mock<IUpworkGraphQLService>();
            _mockJobRepository = new Mock<IJobRepository>();
        }

        [Test]
        public async Task FetchFeeds_And_Process_Should_Call_AddJobs_On_TrelloService()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new ApplicationContext(options);

            //
            
            var processFeedService = new ProcessFeedService(
                _mockLogger.Object,
                context,
                _mockHttpClientFactory.Object,
                _mockCoverletterService.Object,
                _mockUpworkProfileService.Object,
                _mockUpworkGraphQLService.Object,
                _mockJobRepository.Object
            );

            var marketplaceJobPostings = new List<MarketplaceJobPosting>
            {
                new() { Id = "123" }
            };

            var jobPosting = new Job();            

            _mockJobFilterService.Setup(j => j.FilterJobsAsync(It.IsAny<List<MarketplaceJobpostingSearchEdge>>(), It.IsAny<int>()))
                .ReturnsAsync(marketplaceJobPostings);

            _mockCoverletterService.Setup(c => c.CreateCoverLetters(It.IsAny<Task<JobPosting>>()))
                .ReturnsAsync(jobPosting);

            // Act
            await processFeedService.FetchFeeds_And_Process();

            // Assert
            //_mockTrelloService.Verify(t => t.AddJobToBoard(It.IsAny<Job>(),It.IsAny<string>()), Times.Once);
        }
        
        
    }
}