using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.Data;
using ITIAcceptanceProcessSystem.Repositories;

namespace ITIAcceptanceProcessSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IGenericRepository<Candidate> _candidates;
        private IGenericRepository<Instructor> _instructors;
        private IGenericRepository<Admin> _admins;
        private IGenericRepository<ExamScore> _examScores;
        private IGenericRepository<InterviewScore> _interviewScores;

        public IGenericRepository<Candidate> Candidates =>
            _candidates ??= new GenericRepository<Candidate>(_context);

        public IGenericRepository<Instructor> Instructors =>
            _instructors ??= new GenericRepository<Instructor>(_context);

        public IGenericRepository<Admin> Admins =>
            _admins ??= new GenericRepository<Admin>(_context);

                public IGenericRepository<ExamScore> ExamScores =>
            _examScores ??= new ExamScoreRepository(_context);

        public IGenericRepository<InterviewScore> InterviewScores =>
    _interviewScores ??= new InterviewScoreRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
