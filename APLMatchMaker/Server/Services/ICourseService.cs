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
        Task DeleteCourseAsync(int id);
        Task<bool> CourseExistAsync(int courseId);
        Task<bool> StudentExistsAsync(Guid studentId, bool IsSudent);
        Task<bool> RemoveStudentFromCourseAsync(int courseId, Guid studentId);

    }
}
