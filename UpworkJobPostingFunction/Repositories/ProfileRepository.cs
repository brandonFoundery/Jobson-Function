using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Jobson.Models;
using Jobson_Data.Models;

namespace Jobson.Repositories
{
    public interface IUpworkProfileUrlRepository
    {
        Task<UpworkProfile> GetByIdAsync(int id);
        Task<UpworkProfile> GetByProfileTypeIdAsync(int profileTypeId);
        Task<IEnumerable<UpworkProfile>> GetAllAsync();
        Task AddAsync(UpworkProfile upworkProfile);
        Task<UpworkProfile> UpdateAsync(UpworkProfile upworkProfile);
        Task DeleteAsync(int id);
    }

    public class UpworkProfileUrlRepository : IUpworkProfileUrlRepository
    {
        private readonly ApplicationContext _context;

        public UpworkProfileUrlRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UpworkProfile> GetByIdAsync(int id)
        {
            return await _context.UpworkProfiles.FindAsync(id);
        }

        public async Task<UpworkProfile> GetByProfileTypeIdAsync(int profileTypeId)
        {
            return await _context.UpworkProfiles.FirstOrDefaultAsync(x=>x.ProfileTypeId == profileTypeId);
        }

        public async Task<IEnumerable<UpworkProfile>> GetAllAsync()
        {
            return await _context.UpworkProfiles.ToListAsync();
        }

        public async Task AddAsync(UpworkProfile upworkProfile)
        {
            _context.UpworkProfiles.Add(upworkProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<UpworkProfile> UpdateAsync(UpworkProfile upworkProfile)
        {
            _context.UpworkProfiles.Update(upworkProfile);
            await _context.SaveChangesAsync();
            return upworkProfile;
        }

        public async Task DeleteAsync(int id)
        {
            var upworkProfileUrl = await _context.UpworkProfiles.FindAsync(id);
            if (upworkProfileUrl != null)
            {
                _context.UpworkProfiles.Remove(upworkProfileUrl);
                await _context.SaveChangesAsync();
            }
        }
    }

}
