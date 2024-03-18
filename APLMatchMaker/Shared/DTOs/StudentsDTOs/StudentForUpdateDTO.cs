using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentForUpdateDTO : IValidatableObject
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        [MaxLength(256, ErrorMessage = "The email address can contain a maximum of 256 characters.")]
        public string? Email { get; set; } = string.Empty;
        [MinLength(9, ErrorMessage = "The password must contain at least 9 characters.")]
        public string? Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? StudentSocSecNo { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; } = string.Empty;
        public string? CV { get; set; } = string.Empty;
        public int? KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public bool? CVIntro { get; set; }
        public bool? LinkedinIntro { get; set; }
        public bool? Workshopdag { get; set; }
        public bool? APLSamtal { get; set; }
        public string? Checklist { get; set; } = string.Empty;
        public string? CommentByTeacher { get; set; } = string.Empty;
        public string? Language { get; set; }
        public string? Nationality { get; set; }
        public string? Miscellaneous { get; set; } = string.Empty;



        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult(
                "The student must have different first and last names.",
                new[] { "Student" });
            }
        }
    }
}
