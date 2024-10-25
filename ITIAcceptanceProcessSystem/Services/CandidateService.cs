using AutoMapper;
using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.UnitOfWork;

namespace ITIAcceptanceProcessSystem.Services
{
    public class CandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CandidateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task SubmitExamScoreAsync(int candidateId, int iqScore, int englishScore)
        {
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(candidateId);
            if (candidate != null)
            {
                var examScore = new ExamScore
                {
                    CandidateId = candidateId,
                    IQScore = iqScore,
                    EnglishScore = englishScore
                };

                await _unitOfWork.ExamScores.AddAsync(examScore);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<CandidateDto> GetProfileAsync(int candidateId)
        {
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(candidateId);
            if (candidate == null) return null;

            var candidateDto = _mapper.Map<CandidateDto>(candidate);
            return candidateDto;
        }
    }
}
