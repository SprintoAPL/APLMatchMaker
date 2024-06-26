﻿using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Server.Services;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Mvc;

namespace APLMatchMaker.Server.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        //[API to fetch all courses along with search and sort functionality)
        // GET: api/course/?Name=.NET&sortBy=startDate&isAscending=false
        [HttpGet]
        public async Task<IActionResult> GetCoursesAsync([FromQuery] CourseResourceParameters courseParameters)
        {
            try
            {
                var courseDtos = await _courseService.GetCoursesAsync(courseParameters);
                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        // GET: api/course/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCoursesAsync(int id)
        {
            var courseDto = await _courseService.GetCourseByIdAsync(id);

            if (courseDto == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    //return Ok(await _courseService.GetCourseByIdAsync(id));
                    return Ok(courseDto);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal Server Error: {ex.Message}");
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddCourseAsync(CourseDto courseDto)
        {
            try
            {
                if (courseDto == null)
                {
                    return BadRequest("Bad Request: CourseDto is null");
                }


                await _courseService.AddCourseAsync(courseDto);

                //return CreatedAtAction("GetCourseByIdAsync", new { id = courseDto.Id }, courseDto);
                return Ok($"{courseDto.Name} is added.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseAsync(int id, CourseForEditDto courseEdit)
        {
            try
            {
                if (courseEdit == null)
                {
                    return BadRequest("Bad Request: Invalid CourseDto or ID mismatch");
                }

                await _courseService.UpdateCourseAsync(courseEdit, id);
                return Ok($"{courseEdit.Name} is updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            try
            {
                await _courseService.DeleteCourseAsync(id);
                return Ok("The course has been removed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        // Remove student from course!
        [HttpDelete("{courseId}/student/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromCourse(int courseId, Guid studentId)
        {
            if (!await _courseService.CourseExistAsync(courseId))
            {
                return UnprocessableEntity($"No course with id {courseId} exists!");
            }

            if(!await _courseService.StudentExistsAsync(studentId, IsSudent: false)) // Don't check "IsStudent".
            {
                return UnprocessableEntity($"No student with id {studentId} exists!");
            }

            if (!await _courseService.RemoveStudentFromCourseAsync(courseId, studentId))
            {
                return UnprocessableEntity($"No student with id {studentId} is enroled in a course with id {courseId}!");
            }

            return NoContent();
        }


        // Enrole student into course!
        [HttpPost("{courseId}/student/{studentId}")]
        public async Task<IActionResult> EnroleStudentAsync(int courseId, Guid studentId)
        {
            if (!await _courseService.CourseExistAsync(courseId))
            {
                return UnprocessableEntity($"No course with id {courseId} exists!");
            }

            if(!await _courseService.StudentExistsAsync(studentId, IsSudent: true)) // Checks that "IsStudent" is true.
            {
                return UnprocessableEntity($"No student with id {studentId} exists!");
            }

            if (!await _courseService.EnroleStudentAsync(courseId, studentId))
            {
                return UnprocessableEntity($"The student with id {studentId} is alredy enroled in the course with id {courseId}!");
            }

            return NoContent();
        }

    }
}
