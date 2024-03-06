using APLMatchMaker.Server.Services;
using APLMatchMaker.Shared.DTOs;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [Route("GetCoursesAsync")]
        public async Task<IActionResult> GetCoursesAsync()
        {
            try
            {
                var courseDtos = await _courseService.GetAllCoursesAsync();
                return Ok(courseDtos);
            }
            catch (ArgumentNullException ex)
            {

                throw new Exception("Error occurred while retrieving the courses.", ex);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while retrieving the courses. {ex.Message}");
            }
        }
        // GET: CourseController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create


        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while deleting the course with ID {id}.", ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] CourseDto courseDto)
        {
            try
            {
                if (courseDto == null)
                {
                    return BadRequest("Bad Request: CourseDto is null");
                }

                await _courseService.AddCourseAsync(courseDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Error occurred while adding the course {courseDto}. {ex.Message}");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while adding the course{courseDto} {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromBody] CourseDto courseDto)
        {
            try
            {
                if (courseDto == null)
                {
                    return BadRequest("Bad Request: CourseDto is null");
                }

                courseDto.Id = id;

                await _courseService.UpdateCourseAsync(courseDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {

                return BadRequest($"Error occurred while updating the course with ID {courseDto.Id}. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred while updating the course with ID {courseDto.Id}. {ex.Message}");
            }
        }


    }

}