using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APLMatchMaker.Server.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public static UserManager<ApplicationUser> _userManager = default!;
        public StudentRepository(ApplicationDbContext dbContext, IServiceProvider services)
        {
            _db = dbContext;
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        }
        public async Task<List<ApplicationUser>> GetAsync()
        {
            return await _db.ApplicationUsers.Where(au => au.IsStudent == true).ToListAsync();

        }
        public async Task<ApplicationUser?> GetAsync(string id)
        {
            return await _db.ApplicationUsers.FirstOrDefaultAsync(au => au.Id == id && au.IsStudent == true);
        }

        public async Task AddAsync(ApplicationUser _applicationUser, string password)
        {
            await _userManager.CreateAsync(_applicationUser, password);
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
