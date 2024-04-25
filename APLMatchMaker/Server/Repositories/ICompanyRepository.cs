using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;

namespace APLMatchMaker.Server.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesListAsync();

        Task<IEnumerable<Company>> GetCompaniesListAsync(CompanyResourceParameters companyResourceParameters);

        Task<Company> GetCompanyByIdAsync(int id);
        Task<bool> AddCompanyAsync(Company company);
        Task<bool> UpdateCompanyAsync(Company company);
        Task<bool> RemoveCompanyByIdAsync(int id);
        Task<bool> HasEngagementAsync(int id);
        Task<IEnumerable<Company>> GetSortedCompaniesAsync(string sortField, string sortOrder);

    }
}