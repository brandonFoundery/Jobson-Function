using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobson.Models;
using Jobson.Repositories;
using Jobson_Data.Models;

namespace Jobson.Services
{
    public interface IUpworkProfileService
    {
        Task<UpworkProfile> GetByIdAsync(int id);
        Task<IEnumerable<UpworkProfile>> GetAllAsync();
        Task AddAsync(UpworkProfile upworkProfile);
        Task<UpworkProfile> UpdateAsync(UpworkProfile upworkProfile);
        Task<UpworkProfile> GetByProfileTypeIdAsync(int profileTypeId);
        Task DeleteAsync(int id);
    }

    public class UpworkProfileService : IUpworkProfileService
    {
        private readonly IUpworkProfileUrlRepository _repository;
        private readonly ScraperService _scraperService; // Assuming ScraperService is the class for scraping

        public UpworkProfileService(IUpworkProfileUrlRepository repository, ScraperService scraperService)
        {
            _repository = repository;
            _scraperService = scraperService;
        }

        public async Task<UpworkProfile> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<UpworkProfile> GetByProfileTypeIdAsync(int profileTypeId)
        {
            var profile = await _repository.GetByProfileTypeIdAsync(profileTypeId);
            if (profile == null)
            {
                throw new Exception($"Profile not found: ProfileType={profileTypeId}");
            }

            if (string.IsNullOrEmpty(profile.ProfileContent))
            {
                return await UpdateAsync(profile);  //will fetch the profile content
            }

            return profile;
        }

        public async Task<IEnumerable<UpworkProfile>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(UpworkProfile upworkProfile)
        {
            // Scrape the profile content before adding
            var content = await _scraperService.ScrapeUrlAsync(upworkProfile.Url);
            if (content == null)
            {
                throw new Exception($"Failed to scrape profile: ProfileType={upworkProfile.ProfileType}  Url={upworkProfile.Url}");
            }

            upworkProfile.ProfileContent = content;
            await _repository.AddAsync(upworkProfile);
        }

        public async Task<UpworkProfile> UpdateAsync(UpworkProfile upworkProfile)
        {
            // Scrape the profile content before updating
            var content = await _scraperService.ScrapeUrlAsync(upworkProfile.Url);
            if (content == null)
            {
                throw new Exception($"Failed to scrape profile: ProfileType={upworkProfile.ProfileType} Url={upworkProfile.Url}");
            }

            upworkProfile.ProfileContent = content;
            return await _repository.UpdateAsync(upworkProfile);
        }


        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
