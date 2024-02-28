using APLMatchMaker.Shared.DTOs;

namespace APLMatchMaker.Client.Services
{
    public interface ICourseService
    {
        List<CourseDto> GetAllCoursesAsync();
        CourseDto GetCourseByIdAsync(int id);
        void AddCourseAsync(CourseDto course);
        void DeleteCourseAsync(int id);
        void UpdateCourseAsync(CourseDto updatedcourse);
    }
}
