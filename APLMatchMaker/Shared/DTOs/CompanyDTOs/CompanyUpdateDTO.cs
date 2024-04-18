using System.ComponentModel.DataAnnotations;


namespace APLMatchMaker.Shared.DTOs.CompanyDTOs
{
    public class CompanyUpdateDTO
    {
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string OrganizationNumber { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CompanyEmail { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string PostalAdress { get; set; } = string.Empty;

        public string PostalNumber { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        //public string Notes { get; set; } = string.Empty;
    }
}

