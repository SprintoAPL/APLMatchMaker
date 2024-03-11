using System;
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
        public string? FullName { get; set; }
        [MaxLength(256)]
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StudentSocSecNo { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public string? CV { get; set; }
        public int KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public bool CVIntro { get; set; }
        public bool LinkedinIntro { get; set; }
        public bool Workshopdag { get; set; }
        public bool APLSamtal { get; set; }
        public string? Checklist { get; set; }
        public string? CommentByTeacher { get; set; }
        public string? Language { get; set; }
        public string? Nationality { get; set; }
        public string? Miscellaneous { get; set; }
    }
}
