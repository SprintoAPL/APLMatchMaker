using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using APLMatchMaker.Server.Mappings;
using APLMatchMaker.Server.Repositories;
using AutoMapper;


namespace APLMatchMaker.Server.Services
{
    public class CompanyService : ICompanyService
    {
        //-< Properties >-
        private ICompanyRepository _companyRepository;
        private IMapper _mapper;


        //-< Constructor >-
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        
        //-< Get all companies as list >-
        public async Task<IEnumerable<CompanyForListDTO>> GetCompaniesListAsync()
        {
            return _mapper.Map<IEnumerable<CompanyForListDTO>>(
                await _companyRepository.GetCompaniesListAsync());
        }

        // Get a company by ID
        public async Task<CompanyForListDTO> GetCompanyByIdAsync(int id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);
            return _mapper.Map<CompanyForListDTO>(company);
        }

    }
}
