using AutoMapper;
using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.UnitOfWork;

namespace ITIAcceptanceProcessSystem.Services
{
    public class AdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync()
        {
            var instructors = await _unitOfWork.Instructors.GetAllAsync();
            return _mapper.Map<IEnumerable<InstructorDto>>(instructors);
        }

        public async Task AddInstructorAsync(InstructorDto instructorDto)
        {
            var instructor = _mapper.Map<Instructor>(instructorDto);
            await _unitOfWork.Instructors.AddAsync(instructor);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteInstructorAsync(int instructorId)
        {
            await _unitOfWork.Instructors.DeleteAsync(instructorId);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CandidateDto>> GetCandidatesForReviewAsync()
        {
            var candidates = await _unitOfWork.Candidates.GetAllAsync();
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }

        // More admin-related business logic (e.g., managing tests, reviewing analytics, etc.)
    }
}
