using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
namespace APLMatchMaker.Server.Services
{
    public interface ICourseService
    {
        Task<List<CourseForShortListDTO>> GetCoursesAsync(CourseResourceParameters courseParameters); //Service for Get courses along search&sort param
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CourseDto course);
        Task UpdateCourseAsync(CourseForEditDto course, int id);
        Task<bool> DeleteCourseAsync(int id); // Update return type to indicate success/failure

    }
}
