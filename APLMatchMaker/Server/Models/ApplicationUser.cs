using APLMatchMaker.Server.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace APLMatchMaker.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsStudent { get; set; }
        public bool IsCompanyContact { get; set; }
        public bool IsActive { get; set; } = true;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Title { get; set; } = null;
        public string? StudentSocSecNo { get; set; } = null;
        public string? Address { get; set; } = null;
        public int Status { get; set; }
        public DateTime? StatusWhen { get; set; } = null;
        public string? StatusOther { get; set; } = null;
        public string? CV { get; set; } = null;
        public int KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public DateTime? CVIntro { get; set; } = null;
        public DateTime? LinkedinIntro { get; set; } = null;
        public DateTime? Workshopdag { get; set; } = null;
        public DateTime? APLSamtal { get; set; } = null;
        public string? Checklist { get; set; } = null;
        public string? CommentByTeacher { get; set; } = null;
        public string? Language { get; set; } = null;
        public string? Nationality { get; set; } = null;
        public string? Miscellaneous { get; set; } = null;

        // Foreign keys.
        public int? CompanyId { get; set; } = null;


        // Navigation properties.
        public ICollection<Enrollment>? Course { get; set; } = null;
        public ICollection<Internship>? Internships { get; set; } = null;
        public Company? Company { get; set; } = null;
    }
}
