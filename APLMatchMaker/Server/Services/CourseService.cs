using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.EntityFrameworkCore;

namespace APLMatchMaker.Server.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddCourseAsync(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto));
            }

            try
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
            catch (DbUpdateException ex)
            {
                // Log the exception or handle it appropriately
                throw new Exception("Error occurred while adding the course.", ex);
            }
        }

        public async Task DeleteCourseAsync(int id)
        {
            try
            {
                var course = await _dbContext.Courses.FindAsync(id);
                if (course != null)
                {
                    _dbContext.Courses.Remove(course);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while deleting the course with ID {id}.", ex);
            }
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _dbContext.Courses
                    .Select(c => new CourseDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                    }).ToListAsync();

                return courses;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving the courses.", ex);
            }
        }

        public async Task<CourseDto> GetCourseByIdAsync(int id)
        {
            try
            {
                var course = await _dbContext.Courses
                    .Where(c => c.Id == id)
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
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving the course with ID {id}.", ex);
            }
        }

        public async Task UpdateCourseAsync(CourseDto courseDto)
        {
            if (courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto));
            }

            try
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
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while updating the course with ID {courseDto.Id}.", ex);
            }
        }
    }
}
