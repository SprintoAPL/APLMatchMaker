using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.ResourceParameters;

namespace APLMatchMaker.Server.Repositories
{
    public interface IStudentRepository
    {
        Task<bool> AddAsync(ApplicationUser _applicationUser, string password);
        Task<List<ApplicationUser>> GetAsync();
        Task<List<ApplicationUser>> GetAsync(StudentResourceParameters? studentResourceParameters);
        Task<ApplicationUser?> GetAsync(string id);
        Task<bool> RemoveAsync(string id);
        Task<bool> CompleteAsync();
    }
}