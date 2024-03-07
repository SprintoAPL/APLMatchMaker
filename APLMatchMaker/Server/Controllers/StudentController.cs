using APLMatchMaker.Server.Services;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APLMatchMaker.Server.Controllers
{
    [Route("api/student")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentForListDTO>>> GetStudentsAsync()
        {
            return Ok(await _studentService.GetAsync());
        }
    }
}
