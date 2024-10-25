using System.ComponentModel.DataAnnotations;

namespace ITIAcceptanceProcessSystem.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Status { get; set; } = "Pending"; // E.g., "Pending", "Accepted", "Rejected"

        public User User { get; set; }

        public ICollection<InterviewScore> InterviewScores { get; set; }
        public ICollection<ExamScore> ExamScores { get; set; }

    }
}
