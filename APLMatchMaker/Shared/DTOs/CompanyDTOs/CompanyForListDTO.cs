namespace APLMatchMaker.Shared.DTOs.CompanyDTOs
{
    public class CompanyForListDTO
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
    }
}
