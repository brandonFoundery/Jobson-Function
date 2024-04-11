using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using ScrapySharp.Extensions;

namespace Jobson.Services
{
    public interface IScraperService
    {
        Task<string> ScrapeUrlAsync(string url);
    }

    public class ScraperService : IScraperService
    {
        private readonly HttpClient _httpClient;

        public ScraperService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string> ScrapeUrlAsync(string url)
        {
            var httpClient = _httpClient;
            var apiKey = "99d745f4-95e7-4203-b788-a7a8c53786dc";
            var targetUrl = "https://www.upwork.com/freelancers/~01be10919acc4a8307?viewMode=1";

            var requestUrl = $"https://proxy.scrapeops.io/v1/?api_key={apiKey}&url={Uri.EscapeDataString(targetUrl)}";

            try
            {
                var response = await httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // Load the HTML into an HtmlDocument using HtmlAgilityPack
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(content);

                // Use ScrapySharp to parse the HTML
                var sections = htmlDocument.DocumentNode.CssSelect("section.air3-card-section");

                // Combine the sections.select(x=>x.InnerText) into a single string
                var contentText = string.Join(" ", sections.Select(x => x.InnerText));

                return contentText;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
