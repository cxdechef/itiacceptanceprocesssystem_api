using System.ComponentModel.DataAnnotations;

namespace ITIAcceptanceProcessSystem.DTOs
{
    public class InstructorDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Expertise { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
