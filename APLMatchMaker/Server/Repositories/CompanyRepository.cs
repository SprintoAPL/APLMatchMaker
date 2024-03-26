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

    }
}
