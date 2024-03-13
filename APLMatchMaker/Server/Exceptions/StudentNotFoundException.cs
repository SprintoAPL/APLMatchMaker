namespace APLMatchMaker.Server.Exceptions
{
    public class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException(string id) : base($"The student with id: [{id}] doesen't exist")
        {
            
        }
    }

    public class NotFoundException : Exception
    {
        public string Title { get; }
        public NotFoundException(string message, string title = "Not Found") : base(message)
        {
            Title = title;
        }

    }
}
