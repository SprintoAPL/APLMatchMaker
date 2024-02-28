using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace APLMatchMaker.Server.Data
{
    public class SeedData
    {
        public static ApplicationDbContext db = default!;
        public static UserManager<ApplicationUser> userManager = default!;
        public static RoleManager<IdentityRole> roleManager = default!;

        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider services)
        {
            //##-< Setup >-####################################################################
            db = context;

            // if (db.Roles.Any()) return; // Code from earlier project.

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //#################################################################################


            //##-< Course >-###################################################################
            // courses = (Name, Description, StartDate, EndDate)
            var courses = new (string, string, DateTime, DateTime)[]
            {
                (".NET23", "Systemutveckling fullstack i .NET miljö", DateTime.Parse("2023-09-11"), DateTime.Parse("2024-01-31")),
                ("Python24", "Programmering med Python", DateTime.Parse("2024-03-04"), DateTime.Parse("2024-06-28")),
                ("Adobe24", "Förberedande utbildning bildproduktion med Adobes olika verktyg.", DateTime.Parse("2024-03-04"), DateTime.Parse("2024-04-12")),
                ("JAVA24", "Systemutveckling JAVA", DateTime.Parse("2024-04-08"), DateTime.Parse("2024-08-30")),
                ("TEST24", "Systemtesting", DateTime.Parse("2024-05-06"), DateTime.Parse("2024-08-30")),
                (".NET24", "Systemutveckling fullstack i .NET miljö", DateTime.Parse("2024-02-26"), DateTime.Parse("2024-06-28")),
                ("SUPPORT24", "IT-supporttekniker", DateTime.Parse("2024-01-27"), DateTime.Parse("2024-03-27")),
                (".NET24_2", "Systemutveckling fullstack i .NET miljö", DateTime.Parse("2024-09-02"), DateTime.Parse("2024-01-31"))
            };
            await AddCoursesAsync(courses);
            //#################################################################################
        }
        //#####################################################################################



        //##-< Seed Courses Method >-#####################################################################
        private static async Task AddCoursesAsync((string, string, DateTime, DateTime)[] courses)
        {
            string name, description; DateTime startDate, endDate;

            foreach (var course in courses)
            {
                (name, description, startDate, endDate) = course;
                await db.Courses.AddAsync(new Course
                {
                   Name = name,
                   Description = description,
                   StartDate = startDate,
                   EndDate = endDate
                });
            }
            await db.SaveChangesAsync();
        }
        //#####################################################################################
    }
}
