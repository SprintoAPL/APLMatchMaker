using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentForListDTO
    {
        [MaxLength(400)]
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string StudentSocSecNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public string? Language { get; set; }
        public string? Nationality { get; set; }
    }
}
