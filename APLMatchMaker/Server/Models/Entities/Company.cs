using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Server.Models.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string OrganizationNumber { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string CompanyEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; // The company's exchange telephone number.
        public string PostalAdress { get; set; } = string.Empty;
        public int PostalNumber { get; set; }
        public string City { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
