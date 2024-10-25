using System.ComponentModel.DataAnnotations;

namespace ITIAcceptanceProcessSystem.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FullName { get; set; }

        public User User { get; set; }
    }
}
