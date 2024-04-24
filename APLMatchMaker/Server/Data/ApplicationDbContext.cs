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
        public string DefaultPw { get; } = "P@55word!";
        //####-<Data Tables>-#######################################################################
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Internship> Internships => Set<Internship>();
        //##########################################################################################



        //####-< Constructor >-#####################################################################
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }
        //##########################################################################################



        //####-<Combine multiple foreign keys in to one primary key>-###############################
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Enrollment>().HasKey(en => new { en.CourseId, en.ApplicationUserId });
            builder.Entity<Internship>().HasKey(i => new { i.ProjectId, i.ApplicationUserId });
            builder.Entity<ApplicationUser>()
                .HasOne(ap => ap.Company)
                .WithMany(co => co.CompanyContacts)
                .OnDelete(DeleteBehavior.SetNull);
        }
        //##########################################################################################

    }
}
