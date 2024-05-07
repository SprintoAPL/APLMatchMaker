
namespace APLMatchMaker.Server.Repositories
{
    public interface ICourseRepository
    {
        Task<bool> CourseExistsAssynk(int couseId);
        Task<bool> RemoveStudentFromCourse(int courseId, Guid studentId);
        Task<bool> StudentExistsAssync(Guid studentId, bool IsSudent);
        Task<bool> EnroleStudentAsync(int courseId, Guid studentId);
    }
}