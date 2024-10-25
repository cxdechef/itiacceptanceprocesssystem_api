using ITIAcceptanceProcessSystem.Data;
using ITIAcceptanceProcessSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITIAcceptanceProcessSystem.Repositories
{
    public class InterviewScoreRepository : IGenericRepository<InterviewScore>
    {
        private readonly ApplicationDbContext _context;

        public InterviewScoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InterviewScore>> GetAllAsync()
        {
            return await _context.InterviewScores.ToListAsync();
        }

        public async Task<InterviewScore> GetByIdAsync(int id)
        {
            return await _context.InterviewScores.FindAsync(id);
        }

        public async Task AddAsync(InterviewScore interviewScore)
        {
            await _context.InterviewScores.AddAsync(interviewScore);
        }

        public async Task UpdateAsync(InterviewScore interviewScore)
        {
            _context.InterviewScores.Update(interviewScore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var interviewScore = await GetByIdAsync(id);
            if (interviewScore != null)
            {
                _context.InterviewScores.Remove(interviewScore);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InterviewScore>> FindAsync(Expression<Func<InterviewScore, bool>> predicate)
        {
            return await _context.InterviewScores.Where(predicate).ToListAsync(); // Find InterviewScores by condition
        }
    }
}
