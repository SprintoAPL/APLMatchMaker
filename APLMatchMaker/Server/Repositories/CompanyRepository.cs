using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models.Entities;
using APLMatchMaker.Server.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


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

        //method to support Filter & Search
        public async Task<IEnumerable<Company>> GetCompaniesListAsync(CompanyResourceParameters? companyResourceParameters)
        {
            if(companyResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(companyResourceParameters));
            }
            //collection to start from
            var companyCollection = _db.Companies as IQueryable<Company>;
            if(!string.IsNullOrEmpty(companyResourceParameters.CompanyName))
            {
                companyCollection=companyCollection.Where(cc=>cc.CompanyName.Contains(companyResourceParameters.CompanyName.Trim()));
            }
            if (!string.IsNullOrEmpty(companyResourceParameters.PostalAdress))
            {
                companyCollection = companyCollection.Where(cc => cc.PostalAdress.Contains(companyResourceParameters.PostalAdress.Trim()));
            }
            if (!string.IsNullOrEmpty(companyResourceParameters.City))
            {
                companyCollection = companyCollection.Where(cc => cc.City.Contains(companyResourceParameters.City.Trim()));
            }
           if(!string.IsNullOrEmpty(companyResourceParameters.SearchQuery))
            {
                companyCollection = companyCollection.Where(cc =>
                cc.CompanyName.Contains(companyResourceParameters.SearchQuery.ToLower().Trim()) ||
                cc.PostalAdress.Contains(companyResourceParameters.SearchQuery.ToLower().Trim()) ||
                cc.City.Contains(companyResourceParameters.SearchQuery.ToLower().Trim()));

            }
           return await companyCollection.ToListAsync();
        }
        


        // Get a company by ID
        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            var company = await _db.Companies.Where(c => c.Id == id)
                .Include(co => co.CompanyContacts)
                .Include (co => co.Projects!)
                .ThenInclude(pr => pr.Internships!)
                .ThenInclude(i => i.Student!)
                .FirstOrDefaultAsync()
                ?? throw new Exception($"Company with ID {id} not found.");
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

        public async Task<bool> HasEngagementAsync(int id)
        {
            var company = await _db.Companies.Where(co => co.Id == id)
                .Include(co => co.Projects)
                .Include(co => co.CompanyContacts)
                .FirstOrDefaultAsync();

            if (company == null)
            {
                return false;
            }

            return (company.Projects!.Count() > 0) ||
                (company.CompanyContacts!.Count() > 0);
        }
    }
}
