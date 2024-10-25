using ITIAcceptanceProcessSystem.DTOs;

namespace ITIAcceptanceProcessSystem.Services
{
    public interface IAuthService
    {
        Task<string> RegisterCandidateAsync(CandidateDto candidateDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
