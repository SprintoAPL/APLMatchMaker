using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Server.Helpers;

namespace APLMatchMaker.Server.Repositories
{
    public interface IStudentRepository
    {
        Task<bool> AddAsync(ApplicationUser _applicationUser);
        Task<PagedList<ApplicationUser>> GetAsync(StudentResourceParameters? studentResourceParameters);
        Task<ApplicationUser?> GetAsync(Guid id);
        Task<bool> RemoveAsync(Guid id);
        Task<bool> HasEngagementsAsync(Guid id);
        Task<bool> CompleteAsync();
        Task<bool> EmailExistAsync(string email);
        bool UpdateStudent(ApplicationUser studentToUpdate);
    }
}