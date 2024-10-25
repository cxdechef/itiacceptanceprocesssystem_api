using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.UnitOfWork;

namespace ITIAcceptanceProcessSystem.Services
{
    public class InstructorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SubmitInterviewScoreAsync(int candidateId, int instructorId, int technicalScore, int softSkillsScore, string comments)
        {
            var interviewScore = new InterviewScore
            {
                CandidateId = candidateId,
                InstructorId = instructorId,
                TechnicalScore = technicalScore,
                SoftSkillsScore = softSkillsScore,
                Comments = comments
            };

            await _unitOfWork.InterviewScores.AddAsync(interviewScore);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Candidate>> GetIntervieweesAsync()
        {
            return await _unitOfWork.Candidates.GetAllAsync();
        }

        // More instructor-related methods as required
    }
}
