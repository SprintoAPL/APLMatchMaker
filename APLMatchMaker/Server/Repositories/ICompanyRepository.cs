using APLMatchMaker.Server.Models.Entities;

namespace APLMatchMaker.Server.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesListAsync();
        Task<Company> GetCompanyByIdAsync(int id);

        Task<bool> RemoveCompanyByIdAsync(int id);



    }
}