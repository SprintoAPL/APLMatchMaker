﻿using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentForUpdateDTO : IValidatableObject
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //[MaxLength(256, ErrorMessage = "The email address can contain a maximum of 256 characters.")]
        //public string? Email { get; set; } = null!;
        //public string? PhoneNumber { get; set; } = null!;
        public string? StudentSocSecNo { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string? Status { get; set; }=null!;
        public string? CV { get; set; } = null!;
        public int? KnowledgeLevel { get; set; } = null!; // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public bool? CVIntro { get; set; } = null!;
        public bool? LinkedinIntro { get; set; } = null!;
        public bool? Workshopdag { get; set; } = null!;
        public bool? APLSamtal { get; set; } = null!;
        public string? Checklist { get; set; } = null!;
        public string? CommentByTeacher { get; set; } = null!;
        public string? Language { get; set; } = null!;
        public string? Nationality { get; set; } = null!;
        public string? Miscellaneous { get; set; } = null!;



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