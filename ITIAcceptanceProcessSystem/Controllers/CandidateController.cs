using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIAcceptanceProcessSystem.Controllers
{
    [Authorize(Roles = "Candidate")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly CandidateService _candidateService;

        public CandidateController(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost("submit-exam-score")]
        public async Task<IActionResult> SubmitExamScore([FromBody] ExamScoreDto scoreDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _candidateService.SubmitExamScoreAsync(scoreDto.CandidateId, scoreDto.IQScore, scoreDto.EnglishScore);
            return Ok(new { message = "Scores submitted successfully" });
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _candidateService.GetProfileAsync(id);
            if (profile == null) return NotFound();

            return Ok(profile);
        }

        // More candidate-related methods can be added here
    }
}
