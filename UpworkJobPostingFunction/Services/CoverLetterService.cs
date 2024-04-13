using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jobson.Models;
using Jobson.UpworkDTOs;
using Jobson_Data.Models;

namespace Jobson.Services
{
    public interface ICoverletterService
    {
        Task<Job> CreateCoverLetters(Task<JobPosting> jobsToBidJobs);
    }

    public class CoverLetterService: ICoverletterService
    {
        public Task<Job> CreateCoverLetters(Task<JobPosting> jobsToBidJobs)
        {
            throw new NotImplementedException();
        }
    }
}
