using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIAcceptanceProcessSystem.Controllers
{
    [Authorize(Roles = "Instructor")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly InstructorService _instructorService;

        public InstructorController(InstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpPost("submit-interview-score")]
        public async Task<IActionResult> SubmitInterviewScore([FromBody] InterviewScoreDto scoreDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _instructorService.SubmitInterviewScoreAsync(scoreDto.CandidateId, scoreDto.InstructorId, scoreDto.TechnicalScore, scoreDto.SoftSkillsScore, scoreDto.Comments);
            return Ok(new { message = "Interview scores submitted successfully" });
        }

        [HttpGet("interviewees")]
        public async Task<IActionResult> GetInterviewees()
        {
            var interviewees = await _instructorService.GetIntervieweesAsync();
            return Ok(interviewees);
        }

        // More instructor-related methods can be added here
    }
}
