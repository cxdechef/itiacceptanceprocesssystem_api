using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAcceptanceProcessSystem.Models
{
    public class InterviewScore
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        [Required]
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public int TechnicalScore { get; set; }
        public int SoftSkillsScore { get; set; }
        public string Comments { get; set; }

        public Candidate Candidate { get; set; }
        public Instructor Instructor { get; set; }
    }
}
