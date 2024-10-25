using ITIAcceptanceProcessSystem.Constants;
using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Helpers;
using ITIAcceptanceProcessSystem.Models;
using ITIAcceptanceProcessSystem.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace ITIAcceptanceProcessSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHelper _jwtHelper;

        public AuthService(UserManager<User> userManager, IUnitOfWork unitOfWork, JwtHelper jwtHelper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> RegisterCandidateAsync(CandidateDto candidateDto)
        {
            // Create new User for the candidate
            var user = new User
            {
                UserName = candidateDto.Email,
                Email = candidateDto.Email
            };

            // Create user using UserManager
            var result = await _userManager.CreateAsync(user, candidateDto.Password);
            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e => e.Description));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Candidate);

            var candidate = new Candidate
            {
                UserId = user.Id,
                FullName = candidateDto.FullName,
                Status = "Pending"
            };

            await _unitOfWork.Candidates.AddAsync(candidate);
            await _unitOfWork.SaveAsync();

            return "Candidate registered successfully!";
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return null;
            }

            var token = _jwtHelper.GenerateJwtToken(user, UserRoles.Candidate);
            return token;
        }
    }
}
