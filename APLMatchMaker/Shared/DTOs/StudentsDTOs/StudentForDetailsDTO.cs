﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentForDetailsDTO
    {
        [MaxLength(400)]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [MaxLength(256)]
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StudentSocSecNo { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
        public DateTime? StatusWhen { get; set; }
        public string? StatusOther { get; set; }
        public string? CV { get; set; }
        public int KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public DateTime? CVIntro { get; set; }
        public DateTime? LinkedinIntro { get; set; }
        public DateTime? Workshopdag { get; set; }
        public DateTime? APLSamtal { get; set; }
        public string? Checklist { get; set; }
        public string? CommentByTeacher { get; set; }
        public string? Language { get; set; }
        public string? Nationality { get; set; }
        public string? Miscellaneous { get; set; }
        public bool HasEngagement { get; set; }


        // Navigation properties.
        public StudentAtCourseShortDTO? Course { get; set; } = null!;
        public ICollection<StudentInternshipShortListDTO>? Interships { get; set; }
        public StudentWorkAtCompanyShortDTO? WorkAtCompany { get; set; }
    }
}
