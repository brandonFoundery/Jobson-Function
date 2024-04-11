using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobson.UpworkDTOs
{
    public class UpworkDtos
    {
    }

    public class IntRange
    {
        public int RangeStart { get; set; }
        public int RangeEnd { get; set; }
    }

    public class FloatRange
    {
        public float RangeStart { get; set; }
        public float RangeEnd { get; set; }
    }

    public class MarketplaceJobFilter
    {
        public string SearchExpressionEq { get; set; }
        public string SkillExpressionEq { get; set; }
        public string TitleExpressionEq { get; set; }
        // Add other fields as necessary, ensuring they match the GraphQL schema
        public string[] CategoryIdsAny { get; set; }
        public string[] SubcategoryIdsAny { get; set; }
        public string[] OccupationIdsAny { get; set; }
        public string[] OntologySkillIdsAll { get; set; }
        public string SinceIdEq { get; set; }
        public string MaxIdEq { get; set; }
        public string JobTypeEq { get; set; }
        public string[] DurationAny { get; set; }
        public string WorkloadEq { get; set; }
        public IntRange ClientHiresRangeEq { get; set; }
        public FloatRange ClientFeedBackRangeEq { get; set; }
        public IntRange BudgetRangeEq { get; set; }
        public bool? VerifiedPaymentOnlyEq { get; set; }
        public bool? PreviousClientsEq { get; set; }
        public string ExperienceLevelEq { get; set; }
        public string[] LocationsAny { get; set; }
        public string[] CityIdAny { get; set; }
        public string[] ZipCodeIdAny { get; set; }
        public int? RadiusEq { get; set; }
        public string[] AreaIdAny { get; set; }
        public string TimezoneEq { get; set; }
        public string UsStateEq { get; set; }
        public int? DaysPostedEq { get; set; }
        // Other properties based on the GraphQL schema can be added here
    }

    public enum MarketplaceJobPostingSearchType
    {
        USER_JOBS_SEARCH,
        JOBS_FEED,
        USER_SITE_SEARCH_RSS
    }

    public class MarketplaceJobPostingSearchSortAttribute
    {
        public MarketplaceJobPostingSearchSortField Field { get; set; }
    }

    public enum MarketplaceJobPostingSearchSortField
    {
        RECENCY,
        RELEVANCE,
        CLIENT_TOTAL_CHARGE,
        CLIENT_RATING
    }

    public class MarketplaceJobPostingSearchConnection
    {
        public int TotalCount { get; set; }
        public List<MarketplaceJobpostingSearchEdge> Edges { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class MarketplaceJobpostingSearchEdge
    {
        public string Cursor { get; set; }
        public MarketplaceJobPostingSearchResult Node { get; set; }
    }

    public class PageInfo
    {
        public string EndCursor { get; set; }
        public bool HasNextPage { get; set; }
    }

    public class MarketplaceJobPostingSearchResult
    {
        public string Id { get; set; }
        public MarketplaceJobPosting Job { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ciphertext { get; set; }
        public string Duration { get; set; }  // Assuming JobDuration is a string
        public string DurationLabel { get; set; }
        public string Engagement { get; set; }
        public Money Amount { get; set; }  // Assuming Money is a custom class
        public string RecordNumber { get; set; }
        public string ExperienceLevel { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public int FreelancersToHire { get; set; }
        public string Relevance { get; set; }  // Assuming a custom class for this
        public bool Enterprise { get; set; }
        public string RelevanceEncoded { get; set; }
        public int TotalApplicants { get; set; }
        public List<string> PreferredFreelancerLocation { get; set; }
        public bool PreferredFreelancerLocationMandatory { get; set; }
        public bool Premium { get; set; }
        public List<string> ClientNotSureFields { get; set; }
        public List<string> ClientPrivateFields { get; set; }
        public bool Applied { get; set; }
        public string CreatedDateTime { get; set; }
        public string PublishedDateTime { get; set; }
        public string RenewedDateTime { get; set; }
        public MarketplaceJobPostingSearchClientInfo Client { get; set; }  // Assuming a custom class
        public List<MarketplaceJobPostingSearchSkill> Skills { get; set; }  // Assuming a custom class
        public string Occupations { get; set; }  // Assuming a custom class
        public string HourlyBudgetType { get; set; }
        public Money HourlyBudgetMin { get; set; }  // Assuming Money is a custom class
        public Money HourlyBudgetMax { get; set; }  // Assuming Money is a custom class
        public string LocalJobUserDistance { get; set; }
        public Money WeeklyBudget { get; set; }  // Assuming Money is a custom class
        public EngagementDuration EngagementDuration { get; set; }  // Assuming a custom class
        public int? TotalFreelancersToHire { get; set; }
        public string TeamId { get; set; }
        public string FreelancerClientRelation { get; set; }  // Assuming a custom class
    }

    public class MarketplaceJobPosting
    {
        public string Id { get; set; }
        public string WorkFlowState { get; set; } // Assuming a custom enum or class
        public string Ownership { get; set; } // Assuming a custom class
        public string Annotations { get; set; } // Assuming a custom class
        public string ActivityStat { get; set; } // Assuming a custom class
        public MarketplaceJobPostingContent Content { get; set; } // Assuming a custom class
        public List<MarketplacePostingAttachment> Attachments { get; set; } // Assuming a custom class for attachments
        public string Classification { get; set; } // Assuming a custom class
        public string SegmentationData { get; set; } // Assuming a custom class
        public MarketplaceContractTerms ContractTerms { get; set; } // Assuming a custom class
        public MarketplaceContractorSelection ContractorSelection { get; set; } // Assuming a custom class
        public string AdditionalSearchInfo { get; set; } // Assuming a custom class
        public PrivateCompanyInfo ClientCompany { get; set; } // Assuming a custom class
        public MarketplacePublicCompanyInfo ClientCompanyPublic { get; set; } // Assuming a custom class
        public bool CanClientReceiveContractProposal { get; set; }
        public ClientProposalsConnection ClientProposals { get; set; } // Assuming a custom class
        public CustomFieldsConnection CustomFields { get; set; } // Assuming a custom class
    }

    public class PrivateCompanyInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PrivateCompanyLegacyType LegacyType { get; set; }
        public string LogoURL { get; set; }
        public GenericUser ContactUser { get; set; }
        public string Phone { get; set; }
        public string DisplayName { get; set; }
        public bool TeamsEnabled { get; set; }
        public bool CanHire { get; set; }
        public bool Hidden { get; set; }
        public bool IncludeInStats { get; set; }
        public string CompanyName { get; set; }
        public Country Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Timezone { get; set; }
        public string AccountingEntity { get; set; }
        public BillingType BillingType { get; set; }
        public string Summary { get; set; }
        public PaymentVerificationStatus PaymentVerificationStatus { get; set; }
        public PaymentVerificationResult PaymentVerification { get; set; }
        public AgencyDetails AgencyDetails { get; set; }
        public JobPostingConnection JobPosts { get; set; }
    }

    public enum PrivateCompanyLegacyType
    {
        CLIENT,
        AGENCY
    }

    public class MarketplacePublicCompanyInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizationLegacyType LegacyType { get; set; }
        public string LogoURL { get; set; }
        public bool TeamsEnabled { get; set; }
        public bool CanHire { get; set; }
        public bool Hidden { get; set; }
        public bool IncludeInStats { get; set; }
        public Country Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Timezone { get; set; }
        public string AccountingEntity { get; set; }
        public BillingType BillingType { get; set; }
        public PaymentVerificationStatus PaymentVerificationStatus { get; set; }
        public PaymentVerificationResult PaymentVerification { get; set; }
        public AgencyDetails AgencyDetails { get; set; }
    }

    public class ClientProposalsConnection
    {
        public int TotalCount { get; set; }
        public List<ClientProposalsEdge> Edges { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class ClientProposalsEdge
    {
        public string Cursor { get; set; }
        public ClientProposal Node { get; set; }
    }

    public class CustomFieldsConnection
    {
        public List<CustomFieldsEdge> Edges { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class CustomFieldsEdge
    {
        public string Cursor { get; set; }
        public CustomFields Node { get; set; }
    }

    public class GenericUser
    {
        // Properties for GenericUser
    }

    public class Country
    {
        // Properties for Country
    }

    public class BillingType
    {
        // Properties for BillingType
    }

    public class PaymentVerificationStatus
    {
        // Properties for PaymentVerificationStatus
    }

    public class PaymentVerificationResult
    {
        // Properties for PaymentVerificationResult
    }

    public class AgencyDetails
    {
        // Properties for AgencyDetails
    }

    public class JobPostingConnection
    {
        // Properties for JobPostingConnection
    }

    public class OrganizationLegacyType
    {
        // Properties for OrganizationLegacyType
    }

    public class ClientProposal
    {
        // Properties for ClientProposal
    }


    public class CustomFields
    {
        // Properties for CustomFields
    }

    public class MarketplaceContractTerms
    {
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string ContractType { get; set; }
        public string OnSiteType { get; set; }
        public int PersonsToHire { get; set; }
        public string ExperienceLevel { get; set; }
        public FixedPriceContractTerms FixedPriceContractTerms { get; set; }
        public HourlyContractTerms HourlyContractTerms { get; set; }
        public bool NotSurePersonsToHire { get; set; }
        public bool NotSureExperienceLevel { get; set; }

        // Constructor for MarketplaceContractTerms
        public MarketplaceContractTerms(
            string contractStartDate,
            string contractEndDate,
            string contractType,
            string onSiteType,
            int personsToHire,
            string experienceLevel,
            FixedPriceContractTerms fixedPriceContractTerms,
            HourlyContractTerms hourlyContractTerms,
            bool notSurePersonsToHire,
            bool notSureExperienceLevel)
        {
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;
            ContractType = contractType;
            OnSiteType = onSiteType;
            PersonsToHire = personsToHire;
            ExperienceLevel = experienceLevel;
            FixedPriceContractTerms = fixedPriceContractTerms;
            HourlyContractTerms = hourlyContractTerms;
            NotSurePersonsToHire = notSurePersonsToHire;
            NotSureExperienceLevel = notSureExperienceLevel;
        }
    }

    public class FixedPriceContractTerms
    {
        public Money Amount { get; set; }
        public Money MaxAmount { get; set; }
        public EngagementDuration EngagementDuration { get; set; }

        // Constructor to initialize the class properties
        public FixedPriceContractTerms(Money amount, Money maxAmount, EngagementDuration engagementDuration)
        {
            Amount = amount;
            MaxAmount = maxAmount;
            EngagementDuration = engagementDuration;
        }
    }

    public class HourlyContractTerms
    {
        public EngagementDuration EngagementDuration { get; set; }
        public string EngagementType { get; set; }  // Assuming EngagementType is a string; could be an enum if there are specific predefined types
        public bool NotSureProjectDuration { get; set; }
        public string HourlyBudgetType { get; set; }  // Assuming HourlyBudgetType is a string; could be an enum for specific types
        public float HourlyBudgetMin { get; set; }
        public float HourlyBudgetMax { get; set; }

        // Constructor to initialize the class properties
        public HourlyContractTerms(
            EngagementDuration engagementDuration,
            string engagementType,
            bool notSureProjectDuration,
            string hourlyBudgetType,
            float hourlyBudgetMin,
            float hourlyBudgetMax)
        {
            EngagementDuration = engagementDuration;
            EngagementType = engagementType;
            NotSureProjectDuration = notSureProjectDuration;
            HourlyBudgetType = hourlyBudgetType;
            HourlyBudgetMin = hourlyBudgetMin;
            HourlyBudgetMax = hourlyBudgetMax;
        }
    }



    public class MarketplaceContractorSelection
    {
        public MarketplaceProposalRequirements ProposalRequirement { get; set; }
        public MarketplaceQualification Qualification { get; set; }
        public MarketplaceLocation Location { get; set; }

        // Constructor to initialize the properties
        public MarketplaceContractorSelection(
            MarketplaceProposalRequirements proposalRequirement,
            MarketplaceQualification qualification,
            MarketplaceLocation location)
        {
            ProposalRequirement = proposalRequirement;
            Qualification = qualification;
            Location = location;
        }
    }

    public class MarketplaceQualification
    {
        public ContractorType ContractorType { get; set; }
        public EnglishProficiency EnglishProficiency { get; set; }
        public bool HasPortfolio { get; set; }
        public int HoursWorked { get; set; }
        public bool RisingTalent { get; set; }
        public int JobSuccessScore { get; set; }
        public Earning MinEarning { get; set; }
        public List<PreferredGroup> PreferredGroups { get; set; }
        public List<PreferredTest> PreferenceTests { get; set; }
    }

    public enum ContractorType
    {
        ALL,
        INDIVIDUALS,
        AGENCIES
    }

    public enum EnglishProficiency
    {
        ANY,
        BASIC,
        CONVERSATIONAL,
        FLUENT,
        NATIVE
    }

    public enum Earning
    {
        ANY,
        AT_LEAST_1,
        AT_LEAST_100,
        AT_LEAST_1000,
        AT_LEAST_10000
    }

    public class PreferredGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }

    public class PreferredTest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MarketplaceLocation
    {
        public List<string> Countries { get; set; }
        public List<string> States { get; set; }
        public List<string> Timezones { get; set; }
        public bool LocalCheckRequired { get; set; }
        public bool LocalMarket { get; set; }
        public List<Area> Areas { get; set; }
        public bool NotSureLocationPreference { get; set; }
        public string LocalDescription { get; set; }
        public string LocalFlexibilityDescription { get; set; }
    }

    public class Area
    {
        public string Id { get; set; }
        public AreaType AreaType { get; set; }
        public string Name { get; set; }
    }

    public enum AreaType
    {
        CITY,
        AREA,
        POSTAL_CODE
    }

    public class MarketplaceProposalRequirements
    {
        public bool CoverLetterRequired { get; set; }
        public bool FreelancerMilestonesAllowed { get; set; }
        public List<MarketplaceQuestion> ScreeningQuestions { get; set; }

        // Constructor to initialize the class properties
        public MarketplaceProposalRequirements(bool coverLetterRequired, bool freelancerMilestonesAllowed, List<MarketplaceQuestion> screeningQuestions)
        {
            CoverLetterRequired = coverLetterRequired;
            FreelancerMilestonesAllowed = freelancerMilestonesAllowed;
            ScreeningQuestions = screeningQuestions ?? new List<MarketplaceQuestion>();
        }
    }

    public class MarketplaceQuestion
    {
        // Assuming properties of MarketplaceQuestion based on the context
        public string Id { get; set; }
        public string QuestionText { get; set; }
        // Other properties as required

        public MarketplaceQuestion(string id, string questionText)
        {
            Id = id;
            QuestionText = questionText;
        }
    }




    public class MarketplacePostingAttachment
    {
        public string Id { get; set; }
        public int SequenceNumber { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }

        // Constructor to initialize the class properties
        public MarketplacePostingAttachment(string id, int sequenceNumber, string fileName, int fileSize)
        {
            Id = id;
            SequenceNumber = sequenceNumber;
            FileName = fileName;
            FileSize = fileSize;
        }
    }


    public class MarketplaceJobPostingContent
    {
        public string Title { get; set; }
        public string Description { get; set; }

        // Constructor to initialize the class properties
        public MarketplaceJobPostingContent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }


    public class Money
    {
        public string RawValue { get; set; }  // Float point number as a string
        public string Currency { get; set; }  // ISO currency code
        public string DisplayValue { get; set; }  // Display representation

        public Money(string rawValue, string currency, string displayValue)
        {
            RawValue = rawValue;
            Currency = currency;
            DisplayValue = displayValue;
        }

        // Optional: Override ToString to provide a meaningful string representation
        public override string ToString()
        {
            return $"{DisplayValue} ({Currency})";
        }
    }

    public class MarketplaceJobPostingSearchClientInfo
    {
        public string MemberSinceDateTime { get; set; }  // Deprecated legacy field
        public int TotalHires { get; set; }
        public int TotalPostedJobs { get; set; }
        public Money TotalSpent { get; set; }
        public string VerificationStatus { get; set; }
        public int TotalReviews { get; set; }
        public float TotalFeedback { get; set; }
        public string CompanyRid { get; set; }
        public string CompanyName { get; set; }
        public string EdcUserId { get; set; }
        public string LastContractPlatform { get; set; }
        public string LastContractRid { get; set; }
        public string LastContractTitle { get; set; }
        public string CompanyOrgUid { get; set; }
        public bool HasFinancialPrivacy { get; set; }

        // Constructor to initialize the properties
        public MarketplaceJobPostingSearchClientInfo(string memberSinceDateTime, int totalHires, int totalPostedJobs,
            Money totalSpent, string verificationStatus, int totalReviews, float totalFeedback, string companyRid,
            string companyName, string edcUserId, string lastContractPlatform, string lastContractRid,
            string lastContractTitle, string companyOrgUid, bool hasFinancialPrivacy)
        {
            MemberSinceDateTime = memberSinceDateTime;
            TotalHires = totalHires;
            TotalPostedJobs = totalPostedJobs;
            TotalSpent = totalSpent;
            VerificationStatus = verificationStatus;
            TotalReviews = totalReviews;
            TotalFeedback = totalFeedback;
            CompanyRid = companyRid;
            CompanyName = companyName;
            EdcUserId = edcUserId;
            LastContractPlatform = lastContractPlatform;
            LastContractRid = lastContractRid;
            LastContractTitle = lastContractTitle;
            CompanyOrgUid = companyOrgUid;
            HasFinancialPrivacy = hasFinancialPrivacy;
        }
    }

    public class MarketplaceJobPostingSearchSkill
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PrettyName { get; set; }
        public bool Highlighted { get; set; }

        // Constructor to initialize the class properties
        public MarketplaceJobPostingSearchSkill(string id, string name, string prettyName, bool highlighted)
        {
            Id = id;
            Name = name;
            PrettyName = prettyName;
            Highlighted = highlighted;
        }
    }

    public class EngagementDuration
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public int Weeks { get; set; }

        // Constructor to initialize the class properties
        public EngagementDuration(string id, string label, int weeks)
        {
            Id = id;
            Label = label;
            Weeks = weeks;
        }
    }

    public class FreelancerProfile
    {
        public CurrentUser User { get; set; }
        public FreelancerProfilePersonalData PersonalData { get; set; }
        public FreelancerProfileUserPreferences UserPreferences { get; set; }
        public FreelancerProfileOtherExperiencesConnection OtherExperiences { get; set; }
        public FreelancerProfileLanguagesConnection Languages { get; set; }
        public List<FreelancerProfileCertificate> Certificates { get; set; }
        public List<FreelancerProfileEmploymentRecord> EmploymentRecords { get; set; }
        public FreelancerProfileAvailability Availability { get; set; }
        public FreelancerProfileCommittedResponseTime CommittedResponseTime { get; set; }
        public FreelancerProfileProject Project { get; set; }
        public FreelancerSkillsConnection Skills { get; set; }
        public FreelancerProfileAggregates Aggregates { get; set; }
        public FreelancerProfileContract Contract { get; set; }
        public FreelancerProfileContractsConnection Contracts { get; set; }
        public FreelancerProfileJobCategoriesConnection JobCategories { get; set; }
        public FreelancerProfileCompletenessSummary ProfileCompletenessSummary { get; set; }
        public FreelancerProfileLinkedExternalAccountsConnection LinkedExternalAccountsList { get; set; }
        public FreelancerProfileVerifications Verifications { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Country CountryDetails { get; set; }
        public string Email { get; set; }
        public Portrait Portrait { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public PrivateTalentCloudConnection PrivateTalentCloud { get; set; }
    }

    // Supporting classes

    public class CurrentUser
    {
        // Properties for CurrentUser
    }

    public class FreelancerProfilePersonalData
    {
        // Properties for FreelancerProfilePersonalData
    }

    public class FreelancerProfileUserPreferences
    {
        // Properties for FreelancerProfileUserPreferences
    }

    public class FreelancerProfileAvailability
    {
        // Properties for FreelancerProfileAvailability
    }

    public class FreelancerProfileCommittedResponseTime
    {
        // Properties for FreelancerProfileCommittedResponseTime
    }

    public class FreelancerProfileAggregates
    {
        // Properties for FreelancerProfileAggregates
    }

    public class FreelancerProfileContract
    {
        // Properties for FreelancerProfileContract
    }

    public class FreelancerProfileContractsConnection
    {
        // Properties for FreelancerProfileContractsConnection
    }

    public class FreelancerProfileCompletenessSummary
    {
        // Properties for FreelancerProfileCompletenessSummary
    }

    public class FreelancerProfileLinkedExternalAccountsConnection
    {
        // Properties for FreelancerProfileLinkedExternalAccountsConnection
    }

    public class FreelancerProfileVerifications
    {
        // Properties for FreelancerProfileVerifications
    }

    public class PhoneNumber
    {
        // Properties for PhoneNumber
    }

    public class PrivateTalentCloudConnection
    {
        // Properties for PrivateTalentCloudConnection
    }

    public class FreelancerProfileCertificate
    {
        public string Id { get; set; }
        public string EarnedDate { get; set; }
        public string SubmissionCode { get; set; }
        public string Notes { get; set; }
        public string Score { get; set; }
        public bool Active { get; set; }
        public bool Verified { get; set; }
        public string Url { get; set; }
        public string CreatedDateTime { get; set; }
        public string LastUpdatedDateTime { get; set; }
        public string ExpirationDate { get; set; }
        public string ExternalId { get; set; }
    }

    public class FreelancerProfileEmploymentRecord
    {
        public string Id { get; set; }
        public GenericUser User { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string StandardizedCompanyId { get; set; }
        public string StandardizedJobTitleId { get; set; }
        public string Role { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class FreelancerProfileProject
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public FreelancerProfileProjectContractLink ContractLink { get; set; }
        public string ThumbnailId { get; set; }
        public string ThumbnailOriginalId { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailOriginal { get; set; }
        public string ProjectUrl { get; set; }
        public string CompletionDateTime { get; set; }
        public bool Public { get; set; }
        public int Rank { get; set; }
        public List<FreelancerProfileProjectAttachment> Attachments { get; set; }
        public FreelancerSkillsConnection Skills { get; set; }
        public JobCategory Category { get; set; }
        public JobCategory SubCategory { get; set; }
        public string OccupationId { get; set; }
        public FreelancerProfileProjectType ProjectType { get; set; }
        public string Role { get; set; }
        public string ProjectGoal { get; set; }
        public string Solution { get; set; }
        public string PrimaryImageId { get; set; }
        public string CreatedDateTime { get; set; }
    }

    public class FreelancerSkillsConnection
    {
        public List<FreeLancerSkillEdge> Edges { get; set; }
    }

    public class FreelancerProfileJobCategoriesConnection
    {
        public List<FreelancerProfileJobCategoryEdge> Edges { get; set; }
    }

    public class Portrait
    {
        public string Portrait0 { get; set; }
        public string Portrait32 { get; set; }
        public string Portrait50 { get; set; }
        public string Portrait100 { get; set; }
        public string Portrait150 { get; set; }
        public string Portrait500 { get; set; }
    }

    public class FreelancerProfileOtherExperiencesConnection
    {
        public int TotalCount { get; set; }
        public List<FreelancerProfileOtherExperienceEdge> Edges { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class FreelancerProfileLanguagesConnection
    {
        public List<FreelancerProfileLanguageEdge> Edges { get; set; }
    }

    // Supporting classes

 public class FreelancerProfileProjectContractLink
    {
        // Properties for FreelancerProfileProjectContractLink
    }

    public class FreelancerProfileProjectAttachment
    {
        // Properties for FreelancerProfileProjectAttachment
    }

    public enum FreelancerProfileProjectType
    {
        CLASSIC_VIEW,
        CASE_STUDY,
        GALLERY
    }

    public class JobCategory
    {
        // Properties for JobCategory
    }

    public class FreeLancerSkillEdge
    {
        // Properties for FreeLancerSkillEdge
    }

    public class FreelancerProfileJobCategoryEdge
    {
        // Properties for FreelancerProfileJobCategoryEdge
    }

    public class FreelancerProfileOtherExperienceEdge
    {
        // Properties for FreelancerProfileOtherExperienceEdge
    }

    public class FreelancerProfileLanguageEdge
    {
        // Properties for FreelancerProfileLanguageEdge
    }

    #region JobPosting 

    public class JobPosting
    {
        public string Id { get; set; }
        public JobPostingInfo Info { get; set; }
        public AccessType Visibility { get; set; }
        public JobPostingOwnership Ownership { get; set; }
        public JobPostingContent Content { get; set; }
        public List<JobPostingAttachment> Attachment { get; set; }
        public JobPostingClassification Classification { get; set; }
        public JobPostingSegmentationData SegmentationData { get; set; }
        public JobPostingContractTerms ContractTerms { get; set; }
        public JobPostingContractorSelection ContractorSelection { get; set; }
        public JobPostingAdditionalInfo AdditionalInfo { get; set; }
        public JobPostingPtcInfo PtcInfo { get; set; }
        public ProposalsStatistics ProposalsStatistics { get; set; }
        public List<JobPostingCustomFields> CustomFields { get; set; }
    }

    public class JobPostingInfo
    {
        // Add properties based on the JobPostingInfo schema
    }

    public enum AccessType
    {
        PUBLIC_INDEX,
        ACCESS_PUBLIC,
        ACCESS_PRIVATE
    }

    public class JobPostingOwnership
    {
        // Add properties based on the JobPostingOwnership schema
    }

    public class JobPostingContent
    {
        // Add properties based on the JobPostingContent schema
    }

    public class JobPostingAttachment
    {
        // Add properties based on the JobPostingAttachment schema
    }

    public class JobPostingClassification
    {
        // Add properties based on the JobPostingClassification schema
    }

    public class JobPostingSegmentationData
    {
        // Add properties based on the JobPostingSegmentationData schema
    }

    public class JobPostingContractTerms
    {
        // Add properties based on the JobPostingContractTerms schema
    }

    public class JobPostingContractorSelection
    {
        // Add properties based on the JobPostingContractorSelection schema
    }

    public class JobPostingAdditionalInfo
    {
        // Add properties based on the JobPostingAdditionalInfo schema
    }

    public class JobPostingPtcInfo
    {
        // Add properties based on the JobPostingPtcInfo schema
    }

    public class ProposalsStatistics
    {
        // Add properties based on the ProposalsStatistics schema
    }

    public class JobPostingCustomFields
    {
        // Add properties based on the JobPostingCustomFields schema
    }
    #endregion

}
