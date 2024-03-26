using APLMatchMaker.Shared.DTOs.CompanyDTOs;

namespace APLMatchMaker.Server.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyForListDTO>> GetCompaniesListAsync();
        Task<CompanyForListDTO> GetCompanyByIdAsync(int id);

        Task<bool> RemoveCompanyByIdAsync(int id);

        Task<CompanyForCreateDTO> PostAsync(CompanyForCreateDTO dto);

    }
}