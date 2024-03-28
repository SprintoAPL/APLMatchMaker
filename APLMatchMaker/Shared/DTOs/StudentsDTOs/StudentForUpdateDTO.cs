using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{

    public class StudentForUpdateDTO : IValidatableObject
    {
        [Required(ErrorMessage = "You should give the student a first name.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "You should give the student a last name.")]
        public string? LastName { get; set; }
        //[MaxLength(256, ErrorMessage = "The email address can contain a maximum of 256 characters.")]
        //public string? Email { get; set; } = null!;
        //public string? PhoneNumber { get; set; } = null!;
        [Required(ErrorMessage = "You should give the student a social security number.")]
        public string? StudentSocSecNo { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Status { get; set; } = null!;
        public string? CV { get; set; } = null!;
        [Required]
        [Range(0, 4, ErrorMessage = "Knowledge level must be between 0 and 4.")]
        public int? KnowledgeLevel { get; set; } = null!; // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public bool? CVIntro { get; set; } = null!;
        public bool? LinkedinIntro { get; set; } = null!;
        public bool? Workshopdag { get; set; } = null!;
        public bool? APLSamtal { get; set; } = null!;
        public string? Checklist { get; set; } = null!;
        public string? CommentByTeacher { get; set; } = null!;
        [Required(ErrorMessage = "The student should have a language specified.")]
        public string? Language { get; set; } = null!;
        [Required(ErrorMessage = "The student should have a nationality specified.")]
        public string? Nationality { get; set; } = null!;
        public string? Miscellaneous { get; set; } = null!;



        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationcontext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult(
                "the student must have different first and last names.",
                new[] { "student" });
            }
        }
    }
}
