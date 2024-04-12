using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APLMatchMaker.Server.Models.Entities
{
    public class Internship
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty; // Student ID
        public DateTime AlternateStartDate { get; set; }
        public DateTime AlternateEndDate { get; set; }


        // Navigation properties.
        public Project Project { get; set; } = new Project();
        public ApplicationUser Student { get; set; } = new ApplicationUser();
    }
}
