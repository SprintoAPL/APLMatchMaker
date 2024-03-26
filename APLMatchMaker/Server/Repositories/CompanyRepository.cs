using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.EntityFrameworkCore;


namespace APLMatchMaker.Server.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        //-< Properties >-
        ApplicationDbContext _db;

        //-< Constructor >-
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        //-< Get all companies as list >-
        public async Task<IEnumerable<Company>> GetCompaniesListAsync()
        {
            return await _db.Companies.ToListAsync();
        }

        // Get a company by ID
        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            var company = await _db.Companies.FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception($"Company with ID {id} not found.");
            return company;
        }

    }
}
