﻿using APLMatchMaker.Server.Services;
using APLMatchMaker.Shared.DTOs;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public async Task<IActionResult> GetCoursesAsync()
        {
            try
            {
                var courseDtos = await _courseService.GetAllCoursesAsync();

                return Ok(courseDtos);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        // GET: api/course/id
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCoursesAsync(int id)
        {
            return Ok(await _courseService.GetCourseByIdAsync(id));
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

                return CreatedAtAction("GetCourseByIdAsync", new { id = courseDto.Id }, courseDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseAsync(int id, CourseDto courseDto)
        {
            try
            {
                if (courseDto == null || courseDto.Id != id)
                {
                    return BadRequest("Bad Request: Invalid CourseDto or ID mismatch");
                }

                await _courseService.UpdateCourseAsync(courseDto);
                return Ok();
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
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
       

        

      
        
    }
}
