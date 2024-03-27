using APLMatchMaker.Server.Services;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using APLMatchMaker.Server.Models.Entities;

namespace APLMatchMaker.Server.Controllers
{
    [Route("api/company")]
    [ApiController]
    //[Authorize]

    public class CompanyController : ControllerBase
    {
        // Properties 
        private ICompanyService _companyService;


        // Constructor 
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/company
        [HttpGet]
        public async Task<IEnumerable<CompanyForListDTO>> GetCompaniesAsync()
        {
            return await _companyService.GetCompaniesListAsync();
        }


        // GET: api/company/{id}
        [HttpGet("{id}", Name = "GetCompany")]
        public async Task<ActionResult<CompanyForListDTO>> GetCompanyByIdAsync(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound("Company not found."); // Not Found
            }

            return Ok(new { message = "Company found successfully", company }); // Company found successfully
        }


        //POST: api/company
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompanyAsync(CompanyForCreateDTO dto)
        {
            try
            {
                var companyToReturn = await _companyService.PostAsync(dto);
                return CreatedAtAction("GetCompany", new { id = companyToReturn.Id }, "Company added successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request."); // Internal Server Error
            }
        }


        // PUT: api/company/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyUpdateDTO companyUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _companyService.UpdateCompanyAsync(id, companyUpdateDTO);

            if (result)
            {
                return Ok("Company updated successfully."); // Company updated successfully
            }
            else
            {
                return NotFound("Company not found."); // Company not found
            }
        }


        //DELETE: api/company/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompanyByIdAsync(int id)
        {
            var company = await _companyService.RemoveCompanyByIdAsync(id);
            if (company)
            {
                return Ok("Company deleted successfully."); // OK
            }
            else
            {
                return NotFound("Company not found."); // Not Found
            }
        }

    }
}
