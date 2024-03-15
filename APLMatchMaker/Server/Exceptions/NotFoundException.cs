namespace APLMatchMaker.Server.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Title { get; }
        public NotFoundException(string message, string title = "Not Found") : base(message)
        {
            Title = title;
        }

    }
}
