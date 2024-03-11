using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace APLMatchMaker.Server.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<List<ApplicationUser>> GetAsync()
        {
            return await _db.ApplicationUsers.Where(au => au.IsStudent == true).ToListAsync();

        }
        public async Task<ApplicationUser?> GetAsync(string id)
        {
            return await _db.ApplicationUsers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(ApplicationUser _applicationUser)
        {
            await _db.ApplicationUsers.AddAsync(_applicationUser);
        }

        public void Update(ApplicationUser _applicationUser)
        {
            _db.ApplicationUsers.Update(_applicationUser);
        }

        public void Remove(ApplicationUser _applicationUser)
        {
            _db.ApplicationUsers.Remove(_applicationUser);
        }

        public async Task CompleteAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
