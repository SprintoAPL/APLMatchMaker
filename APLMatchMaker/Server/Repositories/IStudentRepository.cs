using APLMatchMaker.Server.Models;

namespace APLMatchMaker.Server.Repositories
{
    public interface IStudentRepository
    {
        Task AddAsync(ApplicationUser _applicationUser, string password);
        Task<List<ApplicationUser>> GetAsync();
        Task<ApplicationUser?> GetAsync(string id);
        void Remove(ApplicationUser _applicationUser);
        void Update(ApplicationUser _applicationUser);
        Task CompleteAsync();
    }
}