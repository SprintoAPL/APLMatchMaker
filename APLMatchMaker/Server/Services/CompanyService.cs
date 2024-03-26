using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using APLMatchMaker.Server.Mappings;
using APLMatchMaker.Server.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using APLMatchMaker.Server.Models.Entities;


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

        //Remove a company by ID

        public async Task<bool> RemoveCompanyByIdAsync(int id)
        {
            var company=await _companyRepository.RemoveCompanyByIdAsync(id);
            return company;
        }

        public async Task<CompanyForCreateDTO> PostAsync(CompanyForCreateDTO dto)
        {
                   var company = new Company
                    {
                        CompanyName = dto.CompanyName,
                        CompanyEmail = dto.CompanyEmail,
                        OrganizationNumber = dto.OrganizationNumber,
                        Phone = dto.Phone,
                        Website = dto.Website,
                        PostalAdress = dto.PostalAdress,
                        PostalNumber = dto.PostalNumber,
                        City = dto.City,
                        //Notes = dto.Notes,
                    };
                    var ok= await _companyRepository.AddCompanyAsync(company);
            if(!ok)
            {
                throw new Exception("Could not add Company!");
            }
            return _mapper.Map<CompanyForCreateDTO>(company);   

        }


    }
}
