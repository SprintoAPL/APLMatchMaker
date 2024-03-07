using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentForListDTO
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public int KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public string? Language { get; set; }
        public string? Nationality { get; set; }
    }
}
