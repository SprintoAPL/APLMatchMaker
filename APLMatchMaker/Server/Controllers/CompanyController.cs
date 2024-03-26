using APLMatchMaker.Server.Services;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace APLMatchMaker.Server.Controllers
{
    [Route("api/company")]
    [ApiController]
    //[Authorize]

    public class CompanyController : ControllerBase
    {
        //-< Properties >-
        private ICompanyService _companyService;


        //-< Constructor >-
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
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyForListDTO>> GetCompanyByIdAsync(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        //DELETE: api/company/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompanyByIdAsync(int id)
        {
            var company = await _companyService.RemoveCompanyByIdAsync(id);
            return company ? Ok(company) : NotFound();

        }
    }
}
