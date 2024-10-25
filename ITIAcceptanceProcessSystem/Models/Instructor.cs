using System.ComponentModel.DataAnnotations;

namespace ITIAcceptanceProcessSystem.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Expertise { get; set; } // E.g., "Technical", "Soft Skills"

        public User User { get; set; }

        public ICollection<InterviewScore> InterviewScores { get; set; }
    }
}
