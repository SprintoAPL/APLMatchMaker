using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.CoursesDTOs
{
    public class CourseForShortListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Properties to track sorting direction
        public bool IsNameSortedAscending { get; set; }
        public bool IsStartDateSortedAscending { get; set; }
        public bool IsEndDateSortedAscending { get; set; }
    }
}
