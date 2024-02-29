using APLMatchMaker.Shared.DTOs;

namespace APLMatchMaker.Server.Services
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CourseDto course);
        Task UpdateCourseAsync(CourseDto course);
        Task DeleteCourseAsync(int id);
    }
}
