using Microsoft.AspNetCore.Identity;

namespace APLMatchMaker.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsStudent { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string StudentSocSecNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CV { get; set; } = string.Empty;
        public int KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public bool CVIntro { get; set; }
        public bool LinkedinIntro { get; set; }
        public bool Workshopdag { get; set; }
        public bool APLSamtal { get; set; }
        public string Checklist { get; set; } = string.Empty;
        public string CommentByTeacher { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Miscellaneous { get; set; } = string.Empty;
        //public override string? Email { get => base.Email; set => base.Email = value; }
        //public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
    }
}
