using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Jobson.Models;
using Jobson.Services;

namespace Jobson.Functions
{
    public class RssFeedMonitorFunction
    {
        private readonly IProcessFeedService _processFeedService;

        public RssFeedMonitorFunction(IProcessFeedService processFeedService)
        {
            _processFeedService = processFeedService;
        }

        [FunctionName("RssFeedMonitor")]
        public async Task Run([TimerTrigger("0 */15 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger logger)
        {
            logger.LogInformation($"RSS feed monitor triggered at: {DateTime.Now}");

            await _processFeedService.FetchFeeds_And_Process();
        }

    }
}
