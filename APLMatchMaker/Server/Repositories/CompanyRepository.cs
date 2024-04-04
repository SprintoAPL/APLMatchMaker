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
        // Properties 
        ApplicationDbContext _db;

        // Constructor 
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Get all companies as list 
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


        //Adding a new company
        public async Task<bool> AddCompanyAsync(Company company)
        {
            if(company!=null)
            {
                await _db.AddAsync(company);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }


        //Update a company 
        public async Task<bool> UpdateCompanyAsync(Company company)
        {
            try
            {
                _db.Entry(company).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }


        //Removing a company by ID
        public async Task<bool> RemoveCompanyByIdAsync(int id)
        {
            var company = _db.Companies.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                return false;
            }
            try
            {
                _db.Companies.Remove(company);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Sorting Of Company
        public async Task<IEnumerable<Company>> GetSortedCompaniesAsync(string sortField, string sortOrder)
        {
            IQueryable<Company> query = _db.Companies;

            switch (sortField.ToLower())
            {
                case "companyname":
                    query = sortOrder.ToLower() == "asc" ?
                        query.OrderBy(c => c.CompanyName) :
                        query.OrderByDescending(c => c.CompanyName);
                    break;
                case "organizationnumber":
                    query = sortOrder.ToLower() == "asc" ?
                        query.OrderBy(c => c.OrganizationNumber) :
                        query.OrderByDescending(c => c.OrganizationNumber);
                    break;
                default:
                    // Default sorting by ID
                    query = sortOrder.ToLower() == "asc" ?
                        query.OrderBy(c => c.Id) :
                        query.OrderByDescending(c => c.Id);
                    break;
            }

            return await query.ToListAsync();
        }

    }
}
