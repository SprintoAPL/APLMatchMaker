namespace APLMatchMaker.Server.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        // Navigation properties.
        public ICollection<Enrollment>? Students { get; set; } = null;

    }
}
