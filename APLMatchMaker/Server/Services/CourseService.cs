using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APLMatchMaker.Server.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseService(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task AddCourseAsync(CourseDto courseDto)
        {
            var course = new Course
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
                StartDate = courseDto.StartDate,
                EndDate = courseDto.EndDate,
            };
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
      
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _dbContext.Courses.FindAsync(id);
            if(course != null)
            {
                _dbContext?.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _dbContext.Courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                StartDate = c.StartDate,
                EndDate = c.EndDate,

            }).ToListAsync();
            return courses;
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            var course =await _dbContext.Courses.Where(c => c.Id == id)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,

                }).FirstOrDefaultAsync();
            return course;
        }

        public async Task UpdateCourseAsync(CourseDto courseDto)
        {
            var course = await _dbContext.Courses.FindAsync(courseDto.Id);
            if (course != null) 
            { 
                course.Name = courseDto.Name;
                course.Description = courseDto.Description;
                course.StartDate = courseDto.StartDate;
                course.EndDate = courseDto.EndDate;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
