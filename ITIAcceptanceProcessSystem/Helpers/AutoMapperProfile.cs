using AutoMapper;
using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace ITIAcceptanceProcessSystem.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Candidate, CandidateDto>().ReverseMap();

            CreateMap<Instructor, InstructorDto>().ReverseMap();

            CreateMap<Admin, AdminDto>().ReverseMap();

            // Map between LoginDto and IdentityUser (for authentication)
            CreateMap<LoginDto, IdentityUser>().ReverseMap();

             CreateMap<InterviewScoreDto, InterviewScore>().ReverseMap();

             CreateMap<ExamScoreDto, ExamScore>().ReverseMap();
        }
    }
}
