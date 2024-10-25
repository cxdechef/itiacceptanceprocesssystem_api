using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ITIAcceptanceProcessSystem.Repositories
{
    public class AdminRepository : IGenericRepository<Admin>
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task AddAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
        }

        public async Task UpdateAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var admin = await GetByIdAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Admin>> FindAsync(Expression<Func<Admin, bool>> predicate)
        {
            return await _context.Admins.Where(predicate).ToListAsync(); // Find admins by condition
        }
    }
}
