using APLMatchMaker.Shared.DTOs.CoursesDTOs;
namespace APLMatchMaker.Server.Services
{
    public interface ICourseService
    {
        Task<List<CourseForShortListDTO>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CourseDto course);
        Task UpdateCourseAsync(CourseForEditDto course, int id);
        Task DeleteCourseAsync(int id);
        Task<List<CourseForShortListDTO>> GetSortedCoursesAsync(string sortBy, bool isAscending);

    }
}
