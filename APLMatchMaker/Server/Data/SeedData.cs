using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //#################################################################################


            //##-< Seed Course Data >-#########################################################
            if (!db.Courses.Any())
            {
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
            }
            //#################################################################################


            //##-< Seed Students data >-#######################################################
            if (await db.ApplicationUsers.Where(au => au.IsStudent == true).FirstOrDefaultAsync() == null)
            {
                //##-< Seed Student Role >-####################################################
                if (!await roleManager.RoleExistsAsync("Student"))
                {
                    var roleNames = new[] { "Student" };
                    await AddRolesAsync(roleNames);
                }
                //#############################################################################

                // (Email, Pw, FirstName, LastName, SosSecNo, Address, Status, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality
                var students = new (string, string, string, string, string, string, string, string, int, string, string, string)[]
                {
                    ("test1@test.se","P@sw0rd1","Eric","Larsson","770404-0099","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk"),
                    ("test2@test.se","P@sw0rd2","Stina","Pettersson","970404-4069","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk"),
                    ("test3@test.se","P@sw0rd3","Eric","Larsson","770404-0099","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk"),
                    ("test4@test.se","P@sw0rd4","Eric","Larsson","770404-0099","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk")
                };
                await AddStudentsAsync(students);
            }
            //#################################################################################
        }

        //#####################################################################################



        //##-< Seed Courses Method >-##########################################################
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



        //##-< Seed Roles Method >-############################################################
        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            }
            await db.SaveChangesAsync();
        }
        //#####################################################################################



        //##-< Seed Students Method >-#########################################################
        private static async Task AddStudentsAsync((string, string, string, string, string, string, string, string, int, string, string, string)[] students)
        {
            string email, pw, firstName, lastName, sosSecNo, address, status, cV, commentByTeacher, language, nationality; int knowledgeLevel;
            foreach (var student in students)
            {
                // (Email, Pw, FirstName, LastName, SosSecNo, Address, Status, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality
                (email, pw, firstName, lastName, sosSecNo, address, status, cV, knowledgeLevel, commentByTeacher, language, nationality) = student;
                var newStudent = new ApplicationUser
                {
                    IsStudent = true,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = firstName,
                    LastName = lastName,
                    StudentSocSecNo = sosSecNo,
                    Address = address,
                    Status = status,
                    CV = cV,
                    KnowledgeLevel = knowledgeLevel,
                    CommentByTeacher = commentByTeacher,
                    Language = language,
                    Nationality = nationality
                };
                var result = await userManager.CreateAsync(newStudent, pw);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

                await AddUserToRoleAsync(newStudent, "Student");
            }
            await db.SaveChangesAsync();
        }
        //#####################################################################################



        //##-< Method to connect a User to a Role >-#########################################################
        private static async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }
        //#####################################################################################
    }
}
