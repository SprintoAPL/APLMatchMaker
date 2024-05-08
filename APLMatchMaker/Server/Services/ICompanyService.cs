using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;

namespace APLMatchMaker.Server.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyForListDTO>> GetCompaniesListAsync();

        Task<IEnumerable<CompanyForListDTO>> GetCompaniesListAsync(CompanyResourceParameters? companyResourceParameters);
        Task<CompanyDetailsDTO> GetCompanyByIdAsync(int id);
        Task<CompanyDetailsDTO> PostAsync(CompanyForCreateDTO dto);
        Task<bool> UpdateCompanyAsync(int id, CompanyUpdateDTO companyUpdateDTO);
        
        Task<bool> RemoveCompanyByIdAsync(int id);
        Task<bool> HasEngagementAsync(int id);
        Task<IEnumerable<CompanyForListDTO>> GetSortedCompaniesListAsync(string sortField, string sortOrder);


    }
}