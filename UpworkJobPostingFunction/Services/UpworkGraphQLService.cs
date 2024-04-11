using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Configuration;
using Jobson.Enums;
using Jobson.UpworkDTOs;

namespace Jobson.Services
{
    public interface IUpworkGraphQLService
    {
        Task<MarketplaceJobPostingSearchConnection> SearchJobsAsync(MarketplaceJobFilter marketPlaceJobFilter,
            MarketplaceJobPostingSearchType searchType, MarketplaceJobPostingSearchSortAttribute[] sortAttributes);

        Task<FreelancerProfile> GetFreelancerProfileByKeyAsync(string profileKey);
        Task<JobPosting> GetJobByIdAsync(string jobPostingId, bool loadAnnotation);
    }

    public class UpworkGraphQLService : IUpworkGraphQLService
    {
        private readonly IConfiguration _configuration;
        private readonly string _upworkApiKey;

        public UpworkGraphQLService(IConfiguration configuration)
        {
            _configuration = configuration;
            _upworkApiKey = _configuration[StringConstants.UpworkApiKey];
        }

        public async Task<MarketplaceJobPostingSearchConnection> SearchJobsAsync(MarketplaceJobFilter marketPlaceJobFilter,
            MarketplaceJobPostingSearchType searchType, MarketplaceJobPostingSearchSortAttribute[] sortAttributes)
        {
            using var client = new GraphQLHttpClient("https://api.upwork.com/graphql", new NewtonsoftJsonSerializer());

            client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_upworkApiKey}");
            client.HttpClient.DefaultRequestHeaders.Add("User-Agent", "SharpenAboutBox");
            var query = new GraphQLHttpRequest
            {
                Query = @"
                                    query marketplaceJobPostings(
                                        $marketPlaceJobFilter: MarketplaceJobFilter,
                                        $searchType: MarketplaceJobPostingSearchType,
                                        $sortAttributes: [MarketplaceJobPostingSearchSortAttribute]
                                    ) {
                                        marketplaceJobPostings(
                                            marketPlaceJobFilter: $marketPlaceJobFilter,
                                            searchType: $searchType,
                                            sortAttributes: $sortAttributes
                                        ) {
                                            totalCount
                                            edges {
                                                ...MarketplaceJobpostingSearchEdgeFragment
                                            }
                                            pageInfo {
                                                ...PageInfoFragment
                                            }
                                        }
                                    }",
                Variables = new
                {
                    marketPlaceJobFilter = marketPlaceJobFilter,
                    searchType = searchType,
                    sortAttributes = sortAttributes
                }
            };
            var response = await client.SendQueryAsync<MarketplaceJobPostingSearchConnection>(query);
            return response.Data;
        }

        public async Task<FreelancerProfile> GetFreelancerProfileByKeyAsync(string profileKey)
        {
            using var client = new GraphQLHttpClient("https://api.upwork.com/graphql", new NewtonsoftJsonSerializer());

            client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_upworkApiKey}");
            client.HttpClient.DefaultRequestHeaders.Add("User-Agent", "SharpenAboutBox");
            var query = new GraphQLHttpRequest
            {
                Query = @"
                query getFreelancerProfile($profileKey: String!) {
                    freelancerProfileByProfileKey(profileKey: $profileKey) {
                        aggregates {
                            ...FreelancerProfileAggregatesFragment
                        }
                        personalData {
                            ...FreelancerProfilePersonalDataFragment
                        }
                    }
                }",
                Variables = new
                {
                    profileKey = profileKey
                }
            };
            var response = await client.SendQueryAsync<FreelancerProfile>(query);
            return response.Data;
        }

        public async Task<JobPosting> GetJobByIdAsync(string jobPostingId, bool loadAnnotation)
        {
            using var client = new GraphQLHttpClient("https://api.upwork.com/graphql", new NewtonsoftJsonSerializer());

            client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {_upworkApiKey}");
            client.HttpClient.DefaultRequestHeaders.Add("User-Agent", "SharpenAboutBox");
            var query = new GraphQLHttpRequest
            {
                Query = @"
                            query jobPosting(
                                $jobPostingId: ID!,
                                $loadAnnotation: Boolean
                            ) {
                                jobPosting(
                                    jobPostingId: $jobPostingId,
                                    loadAnnotation: $loadAnnotation
                                ) {
                                    id
                                    info {
                                        ...JobPostingInfoFragment
                                    }
                                    visibility
                                    ownership {
                                        ...JobPostingOwnershipFragment
                                    }
                                    content {
                                        ...JobPostingContentFragment
                                    }
                                    attachment {
                                        ...JobPostingAttachmentFragment
                                    }
                                    classification {
                                        ...JobPostingClassificationFragment
                                    }
                                    segmentationData {
                                        ...JobPostingSegmentationDataFragment
                                    }
                                    contractTerms {
                                        ...JobPostingContractTermsFragment
                                    }
                                    contractorSelection {
                                        ...JobPostingContractorSelectionFragment
                                    }
                                    additionalInfo {
                                        ...JobPostingAdditionalInfoFragment
                                    }
                                    ptcInfo {
                                        ...JobPostingPtcInfoFragment
                                    }
                                    proposalsStatistics {
                                        ...ProposalsStatisticsFragment
                                    }
                                    customFields {
                                        ...JobPostingCustomFieldsFragment
                                    }
                                }
                            }",
                Variables = new
                {
                    jobPostingId = jobPostingId,
                    loadAnnotation = loadAnnotation
                }
            };
            var response = await client.SendQueryAsync<JobPosting>(query);
            return response.Data;
        }


    }

}
