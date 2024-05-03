namespace APLMatchMaker.Shared.DTOs.CompanyDTOs
{
    public class CompanyDetailsDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string OrganizationNumber { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string CompanyEmail { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PostalAdress { get; set; } = string.Empty;
        public string PostalNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool HasEngagement { get; set; }



        // Navigation Properties.
        public ICollection<CompanyEmployeeShortListDTO>? EmployedStudents { get; set; }
        public ICollection<CompanyContactsShortListDTO>? Contacts { get; set; }
        public ICollection<CompanyInternshipsShortListDTO>? Internships { get; set; }
    }
}
