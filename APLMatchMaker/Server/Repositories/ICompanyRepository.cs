using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;

namespace APLMatchMaker.Server.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesListAsync();
        Task<Company> GetCompanyByIdAsync(int id);
        Task<bool> AddCompanyAsync(Company company);
        Task<bool> UpdateCompanyAsync(Company company);
        Task<bool> RemoveCompanyByIdAsync(int id);




    }
}