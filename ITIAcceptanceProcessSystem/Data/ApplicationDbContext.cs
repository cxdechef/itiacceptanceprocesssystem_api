using ITIAcceptanceProcessSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITIAcceptanceProcessSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ExamScore> ExamScores { get; set; }
        public DbSet<InterviewScore> InterviewScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration here if necessary
        }
    }
}
