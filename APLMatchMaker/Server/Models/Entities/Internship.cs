using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APLMatchMaker.Server.Models.Entities
{
    public class Internship
    {
        public DateTime? AlternateStartDate { get; set; }
        public DateTime? AlternateEndDate { get; set; }


        // Foreign keys.
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty; // Student ID


        // Navigation properties.
        public Project? Project { get; set; } = null;
        public ApplicationUser? Student { get; set; } = null;
    }
}
