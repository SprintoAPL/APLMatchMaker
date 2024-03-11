using APLMatchMaker.Server.Models;

namespace APLMatchMaker.Server.Repositories
{
    public interface IStudentRepository
    {
        Task AddAsync(ApplicationUser _applicationUser, string password);
        Task<List<ApplicationUser>> GetAsync();
        Task<ApplicationUser?> GetAsync(string id);
        Task<bool> RemoveAsync(string id);
        Task CompleteAsync();
    }
}