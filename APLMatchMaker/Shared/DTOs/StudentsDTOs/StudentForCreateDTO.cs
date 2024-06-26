﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentForCreateDTO : IValidatableObject
    {
        [Required(ErrorMessage = "You should give the student a first name.")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should give the student a last name.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "You should give the student a e-mail.")]
        [MaxLength(256, ErrorMessage = "The email address can contain a maximum of 256 characters.")]
        [EmailAddress(ErrorMessage = "The email address must be valid.")]
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? StudentSocSecNo { get; set; }
        public string? City { get; set; }
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
