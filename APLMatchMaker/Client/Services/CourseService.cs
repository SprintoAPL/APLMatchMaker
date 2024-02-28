using APLMatchMaker.Shared.DTOs;

namespace APLMatchMaker.Client.Services
{
    public class CourseService : ICourseService
    {
        public void AddCourseAsync(CourseDto course)
        {
            Random rnd = new Random();
            course.Id = rnd.Next(100000);
            Courses.Add(course);
        }

        public void DeleteCourseAsync(int id)
        {
            var courseToDelete = Courses.FirstOrDefault(c => c.Id == id);
            if(courseToDelete != null)
            {
                courseToDelete.Delete();
            }
        }

        public List<CourseDto> GetAllCoursesAsync()
        {
            return Courses;
        }

        public CourseDto GetCourseByIdAsync(int id)
        {
            return Courses.FirstOrDefault(c => c.Id == Id);  
        }

        public void UpdateCourseAsync(CourseDto updatedcourse)
        {
            var Course = Courses.FirstOrDefault(c => c.Id == updatedcourse.Id);
            if(Course != null)
            {
                Course.Name = updatedcourse.Name;
                Course.Description = updatedcourse.Description;
                Course.StartDate = updatedcourse.StartDate;
                Course.EndDate = updatedcourse.EndDate;
            }
        }
    }
}
