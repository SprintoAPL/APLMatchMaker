namespace APLMatchMaker.Server.Exceptions
{
    public class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException(string id) : base($"The student with id: [{id}] doesen't exist")
        {
            
        }
    }
}
