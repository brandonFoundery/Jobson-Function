using Anthropic.SDK.Constants;
using Anthropic.SDK.Messaging;
using Anthropic.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobson.Enums;
using Jobson.LLMProviders;
using Jobson.Models;
using Jobson.Repositories;
using Jobson.UpworkDTOs;
using Jobson_Data.Models;

namespace Jobson.Services
{
    public interface IJobFilterService
    {
        Task<List<MarketplaceJobPosting>> FilterJobsAsync(List<MarketplaceJobpostingSearchEdge> nonDuplicateJobs,
            long profileTypeId);
    }

    public class JobFilterService: IJobFilterService
    {
        private readonly IScraperService _scraperService;
        private readonly IJobRepository _jobRepository;

        public JobFilterService(IScraperService scraperService, IJobRepository jobRepository)
        {
            _scraperService = scraperService;
            _jobRepository = jobRepository;
        }
        public async Task<List<MarketplaceJobPosting>> FilterJobsAsync(
            List<MarketplaceJobpostingSearchEdge> nonDuplicateJobs,
            long profileTypeId)
        {
            var filteredJobs = new List<MarketplaceJobPosting>();
            foreach (var nonDuplicateJob in nonDuplicateJobs)
            {
                //await GetContent(nonDuplicateJob);
                //await SendChatMessage(profileTypeId);
            }

            return filteredJobs;
        }

        private static async Task SendChatMessage(int profileTypeId)
        {

        }

        private async Task GetContent(Job nonDuplicateJob)
        {
            var content = await ScrapeUrl(nonDuplicateJob);
            //var prompt = 
            //var llmModel = LLMAdapterFactory.CreateAdapter(LLMPromptTypesEnum.JobFilter);
            //var extractedData = llmModel.SendMessage(content);

        }

        private async Task<string> ScrapeUrl(Job nonDuplicateJob)
        {
            var jobContent = await _scraperService.ScrapeUrlAsync(nonDuplicateJob.Link);
            if (jobContent is { Length: > 0 })
            {
                nonDuplicateJob.Content = jobContent;
                await _jobRepository.UpdateAsync(nonDuplicateJob);
            }
            return jobContent;
        }
    }

}
