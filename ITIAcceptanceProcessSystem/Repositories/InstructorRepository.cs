using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ITIAcceptanceProcessSystem.Repositories
{
    public class InstructorRepository : IGenericRepository<Instructor>
    {
        private readonly ApplicationDbContext _context;

        public InstructorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        public async Task AddAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
        }

        public async Task UpdateAsync(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await GetByIdAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Instructor>> FindAsync(Expression<Func<Instructor, bool>> predicate)
        {
            return await _context.Instructors.Where(predicate).ToListAsync(); // Find instructors by condition
        }
    }
}
