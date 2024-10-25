
namespace ITIAcceptanceProcessSystem.DTOs
{
    public class InterviewScoreDto
    {
        public int CandidateId { get; set; }
        public int InstructorId { get; set; }
        public int TechnicalScore { get; set; }
        public int SoftSkillsScore { get; set; }
        public string Comments { get; set; }
    }
}
