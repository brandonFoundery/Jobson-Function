using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Jobson.Models;
using Jobson.Repositories;
using Jobson.UpworkDTOs;
using Jobson_Data.Enums;
using Jobson_Data.Models;
using StringConstants = Jobson.Enums.StringConstants;

namespace Jobson.Services
{
    public interface IProcessFeedService
    {
        Task FetchFeeds_And_Process();
    }

    public class ProcessFeedService : IProcessFeedService
    {
        private readonly ApplicationContext _context;
        private readonly IJobFilterService _filterService;
        private readonly ICoverletterService _coverletterService;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private ICoverletterService _coverLetterService;
        private readonly IUpworkGraphQLService _upworkGraphQlService;
        private readonly IJobRepository _jobRepository;
        private readonly IScraperService _scraperService;
        private readonly IUpworkProfileService _upworkProfileService;

        public ProcessFeedService(ILogger<ProcessFeedService> logger, ApplicationContext context, 
            IHttpClientFactory httpClientFactory,
            //IJobFilterService filterService, 
            ICoverletterService coverletterService, 
            //IScraperService scraperService, 
            IUpworkProfileService upworkProfileService,
            IUpworkGraphQLService upworkGraphQLService,
            IJobRepository jobRepository
            )
        {
            _context = context;
            //_filterService = filterService;
            _coverletterService = coverletterService;
            _upworkGraphQlService = upworkGraphQLService;
            _jobRepository = jobRepository;
            _upworkProfileService = upworkProfileService;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task FetchFeeds_And_Process()
        {
            //Fetch all the profiles from the database
            var profiles = await _upworkProfileService.GetAllAsync();
            foreach (var upworkProfile in profiles)
            {
                //Fetch the UpworkProfile content from UpworkGraphQLService
                var profileContent = await _upworkGraphQlService.GetFreelancerProfileByKeyAsync(upworkProfile.UpworkProfileKey);

                //Create filter based on the profile content
                var marketPlaceJobFilter = CreateFilter_For_Profile(profileContent);

                //Search for jobs using the filter
                var jobs = await _upworkGraphQlService.SearchJobsAsync(marketPlaceJobFilter, MarketplaceJobPostingSearchType.JOBS_FEED, new MarketplaceJobPostingSearchSortAttribute[]
                {
                    new()
                    {
                        Field = MarketplaceJobPostingSearchSortField.RELEVANCE
                    } 
                });

                var jobsRemovedDuplicates = await RemoveDuplicateJobsAsync(jobs);

                //Ask AI to review the jobs and to pick the top 15 jobs
                var topJobs = await _filterService.FilterJobsAsync(jobsRemovedDuplicates, upworkProfile.ProfileTypeId);

                //Create cover letters for the jobs ready to bid
                foreach (var job in topJobs)
                {
                    var fetchJobDetails = _upworkGraphQlService.GetJobByIdAsync(job.Id,true);
                    var jobWithCoverLetter = await _coverletterService.CreateCoverLetters(fetchJobDetails);
                    
                    // Set the job status to ReadyForBid
                    job.Status = JobStatusEnum.ReadyForBid;
                    
                    // Create the job in the database
                    await _jobRepository.AddAsync(jobWithCoverLetter);
                    //Save the job to the database
                    await _context.SaveChangesAsync();
                }

            }
        }

        private async Task<List<MarketplaceJobpostingSearchEdge>> RemoveDuplicateJobsAsync(MarketplaceJobPostingSearchConnection jobsResults)
        {
            var jobs = jobsResults.Edges;
            var jobIds = jobs.Select(j => j.Node.Id).ToList();
            var existingJobs = await _context.Jobs.Where(j => jobIds.Contains(j.UpworkJobId)).ToListAsync();
            var nonDuplicateJobs = jobs
                .Where(j => existingJobs.All(ej => ej.UpworkJobId != j.Node.Id))
                .ToList();
            return nonDuplicateJobs;
        }

        //private async Task CreateNewTrelloBoard(FreelancerProfile profileContent, UpworkProfile upworkProfile)
        //{
        //    var trelloBoard = await _trelloService.CreateOrGetBoardAsync(StringConstants.GetTrelloBoardName(profileContent.FullName));
        //    upworkProfile.TrelloBoardId = trelloBoard.Id;
        //    await _context.TrelloBoards.AddAsync(new TrelloBoard()
        //    {
        //        BoardId = trelloBoard.Id,
        //        Name = trelloBoard.Name,
        //        Url = trelloBoard.Url
        //    });
        //    await _upworkProfileService.UpdateAsync(upworkProfile);
        //    await _context.SaveChangesAsync();
        //}

        private MarketplaceJobFilter CreateFilter_For_Profile(FreelancerProfile profileContent)
        {
            return new MarketplaceJobFilter();
        }

        
    }
}
