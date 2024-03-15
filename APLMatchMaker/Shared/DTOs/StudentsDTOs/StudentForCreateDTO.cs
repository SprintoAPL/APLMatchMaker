using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentForCreateDTO : IValidatableObject
    {
        [Required(ErrorMessage = "You should give the student a first name.")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should give the student a first name.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should give the student a e-mail.")]
        [MaxLength(256, ErrorMessage = "The email address can contain a maximum of 256 characters.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should give the student a password.")]
        [MinLength(9, ErrorMessage = "The password must contain at least 9 characters.")]
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? StudentSocSecNo { get; set; }
        public string? Address { get; set; }
        public string? Language { get; set; }
        public string? Nationality { get; set; }



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
