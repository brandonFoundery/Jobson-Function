using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Jobson.Models;
using Jobson_Data.Models;

namespace Jobson.Repositories;

public interface IPromptRepository
{
    Task<Job> GetByIdAsync(int id);
    Task<IEnumerable<Job>> GetAllAsync();
    Task AddAsync(Job Job);
    Task<Job> UpdateAsync(Job Job);
    Task DeleteAsync(int id);
}

public class PromptRepository : IPromptRepository
{
    private readonly ApplicationContext _context;

    public PromptRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Job> GetByIdAsync(int id)
    {
        return await _context.Jobs.FindAsync(id);
    }

    public async Task<IEnumerable<Job>> GetAllAsync()
    {
        return await _context.Jobs.ToListAsync();
    }

    public async Task AddAsync(Job Job)
    {
        _context.Jobs.Add(Job);
        await _context.SaveChangesAsync();
    }

    public async Task<Job> UpdateAsync(Job Job)
    {
        _context.Jobs.Update(Job);
        await _context.SaveChangesAsync();
        return Job;
    }

    public async Task DeleteAsync(int id)
    {
        var JobUrl = await _context.Jobs.FindAsync(id);
        if (JobUrl != null)
        {
            _context.Jobs.Remove(JobUrl);
            await _context.SaveChangesAsync();
        }
    }

}