using ITIAcceptanceProcessSystem.Data;
using ITIAcceptanceProcessSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITIAcceptanceProcessSystem.Repositories
{
    public class ExamScoreRepository : IGenericRepository<ExamScore>
    {
        private readonly ApplicationDbContext _context;

        public ExamScoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExamScore>> GetAllAsync()
        {
            return await _context.ExamScores.ToListAsync();
        }

        public async Task<ExamScore> GetByIdAsync(int id)
        {
            return await _context.ExamScores.FindAsync(id);
        }

        public async Task AddAsync(ExamScore examScore)
        {
            await _context.ExamScores.AddAsync(examScore);
        }

        public async Task UpdateAsync(ExamScore examScore)
        {
            _context.ExamScores.Update(examScore);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var examScore = await GetByIdAsync(id);
            if (examScore != null)
            {
                _context.ExamScores.Remove(examScore);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ExamScore>> FindAsync(Expression<Func<ExamScore, bool>> predicate)
        {
            return await _context.ExamScores.Where(predicate).ToListAsync(); // Find ExamScores by condition
        }
    }
}
