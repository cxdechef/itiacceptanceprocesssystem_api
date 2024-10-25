using ITIAcceptanceProcessSystem.DTOs;
using ITIAcceptanceProcessSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIAcceptanceProcessSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("instructors")]
        public async Task<IActionResult> GetInstructors()
        {
            var instructors = await _adminService.GetAllInstructorsAsync();
            return Ok(instructors);
        }

        [HttpPost("add-instructor")]
        public async Task<IActionResult> AddInstructor([FromBody] InstructorDto instructorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _adminService.AddInstructorAsync(instructorDto);
            return Ok(new { message = "Instructor added successfully" });
        }

        [HttpDelete("delete-instructor/{id}")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            await _adminService.DeleteInstructorAsync(id);
            return Ok(new { message = "Instructor deleted successfully" });
        }

        [HttpGet("candidates")]
        public async Task<IActionResult> GetCandidatesForReview()
        {
            var candidates = await _adminService.GetCandidatesForReviewAsync();
            return Ok(candidates);
        }

        // More admin-related methods can be added here
    }
}
