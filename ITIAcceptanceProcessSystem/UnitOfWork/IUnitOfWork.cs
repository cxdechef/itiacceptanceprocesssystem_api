using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.Repositories;

namespace ITIAcceptanceProcessSystem.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Candidate> Candidates { get; }
        IGenericRepository<Instructor> Instructors { get; }
        IGenericRepository<Admin> Admins { get; }
        IGenericRepository<ExamScore> ExamScores { get; }
        IGenericRepository<InterviewScore> InterviewScores { get; }
        Task<int> SaveAsync();
    }
}
