using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Server.Helpers;

namespace APLMatchMaker.Server.Repositories
{
    public interface IStudentRepository
    {
        Task<bool> AddAsync(ApplicationUser _applicationUser, string password);
        Task<List<ApplicationUser>> GetAsync();
        Task<PagedList<ApplicationUser>> GetAsync(StudentResourceParameters? studentResourceParameters);
        Task<ApplicationUser?> GetAsync(string id);
        Task<bool> RemoveAsync(string id);
        Task<bool> CompleteAsync();
        Task<bool> EmailExistAsync(string email);
        bool UpdateStudent(ApplicationUser studentToUpdate);
    }
}