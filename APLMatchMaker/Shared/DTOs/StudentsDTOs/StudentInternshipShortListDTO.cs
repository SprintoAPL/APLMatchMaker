using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.StudentsDTOs
{
    public class StudentInternshipShortListDTO
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
    }
}
