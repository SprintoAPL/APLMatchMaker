using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Server.Models.Entities
{
    public class Enrollment
    {
        // Foreign keys.
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;


        // Navigation properties.
        public ApplicationUser? Student { get; set; } = null!;
        public Course? Course { get; set; } = null!;
    }
}
