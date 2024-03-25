using APLMatchMaker.Shared.DTOs.CoursesDTOs;
namespace APLMatchMaker.Server.Services
{
    public interface ICourseService
    {
        Task<List<CourseForShortListDTO>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CourseDto course);
        Task UpdateCourseAsync(CourseDto course);
        Task DeleteCourseAsync(int id);
    }
}
