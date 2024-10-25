using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIAcceptanceProcessSystem.Models
{
    public class ExamScore
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }

        public int IQScore { get; set; }
        public int EnglishScore { get; set; }

        public Candidate Candidate { get; set; }
    }
}
