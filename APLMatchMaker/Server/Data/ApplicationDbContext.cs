using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Models.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace APLMatchMaker.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        //####-<Data Tables>-#######################################################################
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        //##########################################################################################

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
          
        }

    }
}
