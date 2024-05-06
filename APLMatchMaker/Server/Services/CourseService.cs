using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Exceptions;
using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.ResourceParameters;
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


        //Course Deletion logic with some rules(EndDate check and Student Check)
        public async Task DeleteCourseAsync(int id)
        {
            try
            {
                var course = await _dbContext.Courses.FindAsync(id);
                if (course == null)
                {
                    throw new KeyNotFoundException($"Course with ID {id} not found.");
                }

                // Check if course can be deleted based on business rules
                bool canDeleteCourse = CanDeleteCourse(course);

                if (!canDeleteCourse)
                {
                    throw new InvalidOperationException("Course cannot be deleted because it has enrolled students or its end date is not in the past.");
                }

                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw new NotFoundException($"Course with ID {id} not found.");
            }
            catch (InvalidOperationException ex)
            {
                // Course cannot be deleted due to business rule violation
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions 
                throw new Exception($"Error occurred while deleting the course with ID {id}.", ex);
            }
        }

        private bool CanDeleteCourse(Course course)
        {
            // Check if the course end date is in the past
            bool endDateIsInPast = course.EndDate < DateTime.Today;

            // Check if the course has no associated students
            bool noEnrolledStudents = course.Students == null || course.Students.Count == 0;

            // Course can be deleted if both conditions are true
            return endDateIsInPast && noEnrolledStudents;
        }




        //Course Service for Getting List of Courses along with search and sort option:
        public async Task<List<CourseForShortListDTO>> GetCoursesAsync(CourseResourceParameters courseParameters)
        {
            try
            {
                IQueryable<Course> coursesQuery = _dbContext.Courses;

                // Apply filters based on search query and specific fields
                if (!string.IsNullOrWhiteSpace(courseParameters.Name))
                {
                    coursesQuery = coursesQuery.Where(c => c.Name.Contains(courseParameters.Name.Trim()));
                }

                if (courseParameters.StartDate != DateTime.MinValue)
                {
                    coursesQuery = coursesQuery.Where(c => c.StartDate == courseParameters.StartDate);
                }

                if (!string.IsNullOrWhiteSpace(courseParameters.SearchQuery))
                {
                    coursesQuery = coursesQuery.Where(c => c.Name.Contains(courseParameters.SearchQuery.Trim()));
                }

                // Apply sorting based on sort parameters
                coursesQuery = ApplySort(coursesQuery, courseParameters.SortBy, courseParameters.IsAscending);

                // Project the result into CourseForShortListDTO
                var courses = await coursesQuery
                    .Select(c => new CourseForShortListDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                    })
                    .ToListAsync();

                return courses;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving and sorting courses.", ex);
            }
        }
        //ApplySort Method for sorting in GetCoursesAsync Service:
        private IQueryable<Course> ApplySort(IQueryable<Course> courses, string sortBy, bool isAscending)
        {
            switch (sortBy.ToLower())
            {
                case "name":
                    return isAscending ? courses.OrderBy(c => c.Name) : courses.OrderByDescending(c => c.Name);
                case "startdate":
                    return isAscending ? courses.OrderBy(c => c.StartDate) : courses.OrderByDescending(c => c.StartDate);
                case "enddate":
                    return isAscending ? courses.OrderBy(c => c.EndDate) : courses.OrderByDescending(c => c.EndDate);
                case "id":
                    return isAscending ? courses.OrderBy(c => c.Id) : courses.OrderByDescending(c => c.Id);
                default:
                    return courses.OrderBy(c => c.Id); // Default sorting by ID
            }
        }



        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            try
            {
                var course = await _dbContext.Courses
                    .Where(c => c.Id == id)
                    .Include(c => c.Students!)
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
                            StudentSocSecNo = s.Student.StudentSocSecNo!,
                            Address = s.Student.Address!,
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

    }
}
