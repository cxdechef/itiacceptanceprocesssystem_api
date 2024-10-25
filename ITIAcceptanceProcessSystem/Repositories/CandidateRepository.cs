using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ITIAcceptanceProcessSystem.Repositories
{
    public class CandidateRepository : IGenericRepository<Candidate>
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await _context.Candidates.FindAsync(id);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var candidate = await GetByIdAsync(id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Candidate>> FindAsync(Expression<Func<Candidate, bool>> predicate)
        {
            return await _context.Candidates.Where(predicate).ToListAsync(); // Find candidates by condition
        }
    }
}
