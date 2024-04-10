namespace APLMatchMaker.Server.ResourceParameters
{
    public class CourseResourceParameters
    {
        //Filter parameters
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }

        //Search paramters
        public string? SearchQuery { get; set; }


    }
}
