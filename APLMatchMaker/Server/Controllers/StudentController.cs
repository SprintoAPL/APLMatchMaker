using APLMatchMaker.Server.Services;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace APLMatchMaker.Server.Controllers
{
    [Route("api/student")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        //##-< Properties >-###############################################################
        private readonly IStudentService _studentService;
        //#################################################################################


        //##-< Constructor >-##############################################################
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        //#################################################################################


        //##-< Get list of students (includes search and filter) >-########################

        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentForListDTO>>> GetStudentsAsync(
            [FromQuery] StudentResourceParameters? studentResourceParameters)
        {
            return Ok(await _studentService.GetAsync(studentResourceParameters));
        }
        //#################################################################################


        //##-< Gets an student with id >-##################################################

        // GET: api/student/id
        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<ActionResult<StudentForDetailsDTO>> GetStudentsAsync(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        //#################################################################################


        //##-< Posts a new student >-######################################################

        // POST: api/student
        [HttpPost]
        public async Task<ActionResult<StudentForDetailsDTO>> PostStudentAsync(StudentForCreateDTO dto)
        {
            if (await _studentService.EmailExistAsync(dto.Email))
            {
                return UnprocessableEntity("Email exists!");
            }
            var studentToReturn = await _studentService.PostAsync(dto);
            return CreatedAtRoute("GetStudent", new { Id = studentToReturn.Id }, studentToReturn);
        }
        //#################################################################################


        //##-< Updates a student (with PATCH:) with id >-##################################

        // PATCH: api/student/id
        [HttpPatch("{id}")]
        public async Task<ActionResult<StudentForDetailsDTO>> PartiallyUpdateStudentAsync(string id, JsonPatchDocument<StudentForUpdateDTO> _patchDocument)
        {
            var _studentToPatch = await _studentService.GetForUpdateAsync(id);
            if (_studentToPatch == null)
            {
                return NoContent();
            }
            _patchDocument.ApplyTo(_studentToPatch, ModelState);

            if (!TryValidateModel(_studentToPatch))
            {
                return ValidationProblem(ModelState);
            }

            return Ok(await _studentService.UpdateStudentAsync(id, _studentToPatch));
            //return CreatedAtRoute("GetStudent", new { Id = studentToReturn.Id }, studentToReturn);
        }
        //#################################################################################


        //##-< Removes a student with id >-################################################

        // DELETE: api/student/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentAsync(string id)
        {
            var result = await _studentService.RemoveAsync(id);
            return result ? NoContent() : NotFound();
        }
        //#################################################################################


        //##-< ???????????? >-#############################################################
        // New methods goes here.
        //#################################################################################


        //##-< Improve returned validation messages >-#####################################
        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value
                .InvalidModelStateResponseFactory(ControllerContext);
        }
        //#################################################################################
    }
}
