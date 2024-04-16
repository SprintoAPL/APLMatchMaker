using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
namespace APLMatchMaker.Server.Services
{
    public interface ICourseService
    {
        Task<List<CourseForShortListDTO>> GetAllCoursesAsync();

        Task<List<CourseForShortListDTO>> GetSearchedCoursesAsync(CourseResourceParameters? courseResourceParameters);
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CourseDto course);
        Task UpdateCourseAsync(CourseForEditDto course, int id);
        Task DeleteCourseAsync(int id);
    }
}
