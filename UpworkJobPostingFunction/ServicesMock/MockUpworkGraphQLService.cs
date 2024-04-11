using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobson.Services;
using Jobson.UpworkDTOs;

namespace Jobson.ServicesMock
{
    public class MockUpworkGraphQLService: IUpworkGraphQLService
    {
        public Task<MarketplaceJobPostingSearchConnection> SearchJobsAsync(MarketplaceJobFilter marketPlaceJobFilter, MarketplaceJobPostingSearchType searchType,
            MarketplaceJobPostingSearchSortAttribute[] sortAttributes)
        {
            throw new NotImplementedException();
        }

        public Task<FreelancerProfile> GetFreelancerProfileByKeyAsync(string profileKey)
        {
            throw new NotImplementedException();
        }

        public Task<JobPosting> GetJobByIdAsync(string jobPostingId, bool loadAnnotation)
        {
            throw new NotImplementedException();
        }
    }
}
