using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.CompanyDTOs
{
    public class CompanyForListDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Orgnumber { get; set; } = string.Empty;
        public string Email{ get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; // The company's exchange telephone number.
        


        //public int Id { get; set; }
        //public string CompanyName { get; set; } = string.Empty;
        //public string Website { get; set; } = string.Empty;
        //public string CompanyEmail { get; set; } = string.Empty;
        //public string Phone { get; set; } = string.Empty; // The company's exchange telephone number.
        //public string PostalAdress { get; set; } = string.Empty;
    }
}
