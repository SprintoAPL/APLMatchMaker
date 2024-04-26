using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.CompanyDTOs
{
    public class CompanyInternshipsShortListDTO
    {
        //public string? UserId { get; set; }
        //public string? FullName { get; set; }
        //public int ProjectId { get; set; }
        //public string? ProjectName { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set;}


        public DateTime? AlternateStartDate { get; set; }
        public DateTime? AlternateEndDate { get; set; }


        // Foreign keys.
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty; // Student ID
    }
}
