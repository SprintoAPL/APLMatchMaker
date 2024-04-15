using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
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

        public async Task<List<CourseForShortListDTO>> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _dbContext.Courses
                    .Select(c => new CourseForShortListDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
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

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            try
            {
                var course = await _dbContext.Courses
                    .Where(c => c.Id == id)
                    .Include(c => c.Students)
                    .ThenInclude(s => s.Student)
                    .Select(c => new CourseDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                        students = (ICollection<StudentForListDTO>)c.Students!.Select(s => new StudentForListDTO 
                        {
                            Id = s.Student!.Id,
                            Name = $"{s.Student.FirstName} {s.Student.LastName}",
                            Email = s.Student.Email,
                            PhoneNumber = s.Student.PhoneNumber,
                            StudentSocSecNo = s.Student.StudentSocSecNo,
                            Address = s.Student.Address,
                            KnowledgeLevel = s.Student.KnowledgeLevel,
                            Language = s.Student.Language,
                            Nationality = s.Student.Nationality
                        })
                    }).FirstOrDefaultAsync();

                return course;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while retrieving the course with ID {id}.", ex);
            }
        }

        public async Task UpdateCourseAsync(CourseForEditDto courseDto, int id)
        {
            if (courseDto == null)
            {
                throw new ArgumentNullException(nameof(courseDto));
            }

            try
            {
                
                var course = await _dbContext.Courses.FindAsync(id);
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
                throw new Exception($"Error occurred while updating the course with ID {id}.", ex);
            }
        }

        //Sorting Of Course (isAscending=true == ascOrd/ isAscending=false == dscOrd)
        public async Task<List<CourseForShortListDTO>> GetSortedCoursesAsync(string sortBy, bool isAscending)
        {
            IQueryable<Course> courses = _dbContext.Courses;

            switch (sortBy.ToLower())
            {
                case "name":
                    courses = isAscending ? courses.OrderBy(c => c.Name) : courses.OrderByDescending(c => c.Name);
                    break;
                case "id":
                    courses = isAscending ? courses.OrderBy(c => c.Id) : courses.OrderByDescending(c => c.Id);
                    break;
                case "startdate":
                    courses = isAscending ? courses.OrderBy(c => c.StartDate) : courses.OrderByDescending(c => c.StartDate);
                    break;
                default:
                    courses = courses.OrderBy(c => c.Id); // Default sorting by ID
                    break;
            }
            // Project the result into CourseForShortListDTO
            return await courses
                .Select(c => new CourseForShortListDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                })
                .ToListAsync();
        }


    }
}
