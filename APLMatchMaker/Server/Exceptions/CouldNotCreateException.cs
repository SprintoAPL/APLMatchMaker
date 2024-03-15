namespace APLMatchMaker.Server.Exceptions
{
    public class CouldNotCreateException : Exception
    {
        public string Title { get; set; }
        public CouldNotCreateException(string message, string title = "Could not create.") : base(message)
        {
            Title = title;
        }
    }
}
