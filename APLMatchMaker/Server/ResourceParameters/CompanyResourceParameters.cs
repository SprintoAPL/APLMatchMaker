namespace APLMatchMaker.Server.ResourceParameters
{
    public class CompanyResourceParameters
    {
        //Filter parameters
        public string CompanyName { get; set; } = string.Empty;
        public string PostalAdress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        //Search parameters
        public string? SearchQuery { get; set; }
    }
}
