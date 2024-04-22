using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Server.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;
        public int NumberOfInterns { get; set; }
        public DateTime DefaultStartDate { get; set; }
        public DateTime DefaultEndDate { get; set; }


        // Foreign keys
        [Required]
        public int CompanyId { get; set; }


        // Navigation properties.
        public Company? Company { get; set; } = null;
        public ICollection<Internship>? Internships { get; set; } = null;
    }
}
