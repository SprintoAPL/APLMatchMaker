using APLMatchMaker.Server.Services;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Server.Helpers;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Text.Json;

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
        [HttpGet(Name = "GetStudents")] // Plural
        public async Task<ActionResult<IEnumerable<StudentForListDTO>>> GetStudentsAsync(
            [FromQuery] StudentResourceParameters? studentResourceParameters)
        {
            PagingFactoids _pagingFactoids; IEnumerable<StudentForListDTO> _studentsForList;
            (_studentsForList, _pagingFactoids) = await _studentService.GetAsync(studentResourceParameters);

            var _previousPageLink = _pagingFactoids.HasPrevious
                ? CreateStudentsResourceUri(
                    studentResourceParameters!,
                    ResourceUriType.PreviousPage) : null;

            var _nextPageLink = _pagingFactoids.HasNext
                ? CreateStudentsResourceUri(
                    studentResourceParameters!,
                    ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                TotalCount = _pagingFactoids.TotalCount,
                PageSize =  _pagingFactoids.PageSize,
                CurrentPage = _pagingFactoids.CurrentPage,
                TotalPages = _pagingFactoids.TotalPages,
                PreviousPageLink = _previousPageLink,
                NextPageLink = _nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_studentsForList);
        }
        //#################################################################################


        //##-< Resource helper >-##########################################################
        private string? CreateStudentsResourceUri(
            StudentResourceParameters studentResourceParameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.NextPage:
                    return Url.Link("GetStudents",
                        new
                        {
                            PageNumber = studentResourceParameters.PageNumber + 1,
                            PageSize = studentResourceParameters.PageSize,

                            Address = studentResourceParameters.Address,
                            Status = studentResourceParameters.Status,
                            KnowledgeLevel = studentResourceParameters.KnowledgeLevel,
                            CVIntro = studentResourceParameters.CVIntro,
                            LinkedinIntro = studentResourceParameters.LinkedinIntro,
                            Workshopdag = studentResourceParameters.Workshopdag,
                            APLSamtal = studentResourceParameters.APLSamtal,
                            Language = studentResourceParameters.Language,
                            Nationality = studentResourceParameters.Nationality,

                            SearchQuery = studentResourceParameters.SearchQuery,

                            OrderBy = studentResourceParameters.OrderBy,
                        });
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetStudents",
                        new
                        {
                            PageNumber = studentResourceParameters.PageNumber - 1,
                            PageSize = studentResourceParameters.PageSize,

                            Address = studentResourceParameters.Address,
                            Status = studentResourceParameters.Status,
                            KnowledgeLevel = studentResourceParameters.KnowledgeLevel,
                            CVIntro = studentResourceParameters.CVIntro,
                            LinkedinIntro = studentResourceParameters.LinkedinIntro,
                            Workshopdag = studentResourceParameters.Workshopdag,
                            APLSamtal = studentResourceParameters.APLSamtal,
                            Language = studentResourceParameters.Language,
                            Nationality = studentResourceParameters.Nationality,

                            SearchQuery = studentResourceParameters.SearchQuery,

                            OrderBy = studentResourceParameters.OrderBy,
                        });
                default:
                    return Url.Link("GetStudents",
                        new
                        {
                            PageNumber = studentResourceParameters.PageNumber,
                            PageSize = studentResourceParameters.PageSize,

                            Address = studentResourceParameters.Address,
                            Status = studentResourceParameters.Status,
                            KnowledgeLevel = studentResourceParameters.KnowledgeLevel,
                            CVIntro = studentResourceParameters.CVIntro,
                            LinkedinIntro = studentResourceParameters.LinkedinIntro,
                            Workshopdag = studentResourceParameters.Workshopdag,
                            APLSamtal = studentResourceParameters.APLSamtal,
                            Language = studentResourceParameters.Language,
                            Nationality = studentResourceParameters.Nationality,

                            SearchQuery = studentResourceParameters.SearchQuery,

                            OrderBy = studentResourceParameters.OrderBy,
                        });
            }
        }
        //#################################################################################


        //##-< Gets an student with id >-##################################################

        // GET: api/student/id
        [HttpGet("{id}", Name = "GetStudent")]  // Singular
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


        //##-< Updates a student (with PUT:) with id >-####################################

        // PUT: api/student/id
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentForDetailsDTO>> FullyUpdateStudentAsync(string id, StudentForUpdateDTO updatedStudent)
        {
            var result = await _studentService.UpdateStudentAsync(id, updatedStudent);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
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

            return Ok(await _studentService.PatchStudentAsync(id, _studentToPatch));
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
