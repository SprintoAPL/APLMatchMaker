using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.CompanyDTOs
{
    public class CompanyForCreateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Company should have a valid name")]
        public string CompanyName { get; set; } = string.Empty;
        [MaxLength(15)]
        [Required]
        public string OrganizationNumber { get; set; } = string.Empty;
        
        public string Website { get; set; } = string.Empty;
        [MaxLength(50, ErrorMessage ="Email address can contain a maximum of 50 characters")]
        public string CompanyEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; 
        public string PostalAdress { get; set; } = string.Empty;
        public int PostalNumber { get; set; }
        public string City { get; set; } = string.Empty;
        //public string Notes { get; set; } = string.Empty;
    }
}
