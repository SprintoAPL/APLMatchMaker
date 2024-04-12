using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APLMatchMaker.Server.Data
{
    public class SeedData
    {
        //##-< Properties >-###############################################################
        public static ApplicationDbContext db = default!;
        public static UserManager<ApplicationUser> userManager = default!;
        public static RoleManager<IdentityRole> roleManager = default!;
        //#################################################################################

        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider services)
        {
            //##-< Setup >-####################################################################
            db = context;

            // if (db.Roles.Any()) return; // Code from earlier project.

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //#################################################################################


            //##-< Seed Course Data >-#########################################################
            if (!await db.Courses.AnyAsync())
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
                    (".NET24_2", "Systemutveckling fullstack i .NET miljö", DateTime.Parse("2024-09-02"), DateTime.Parse("2024-01-31")),
                    ("Computer Science", "An introductory module covering the basics of programming, including syntax, control structures, and algorithmic problem-solving.", DateTime.Parse("2024-01-05"), DateTime.Parse("2024-02-04")),
                    ("Psychology", "Explores abnormal behavior, mental disorders, diagnostic criteria, treatment approaches, and societal perspectives on mental health.", DateTime.Parse("2024-01-10"), DateTime.Parse("2024-02-24")),
                    ("Biomedical Engineering", "Focuses on materials used in medical devices and implants, emphasizing properties, biocompatibility, and design considerations.", DateTime.Parse("2024-01-15"), DateTime.Parse("2024-03-15")),
                    ("Environmental Science", "Examines the development and implementation of environmental policies, regulations, and laws, considering the roles of government and organizations.", DateTime.Parse("2024-01-20"), DateTime.Parse("2024-02-19")),
                    ("Business Administration", "Covers fundamental marketing principles, including market analysis, consumer behavior, product development, pricing strategies, and promotional activities.", DateTime.Parse("2024-01-20"), DateTime.Parse("2024-02-19")),
                    ("Political Science", "Explores different political systems, institutions, and practices across countries, facilitating comparison and contrast of political structures and processes.", DateTime.Parse("2024-01-20"), DateTime.Parse("2024-02-19")),
                    ("Civil Engineering", "Focuses on soil mechanics and its applications in engineering, covering topics such as soil properties, foundation design, and slope stability analysis.", DateTime.Parse("2024-01-20"), DateTime.Parse("2024-02-19")),
                    ("Economics", "Applies statistical methods to economic data, teaching students how to analyze and interpret economic phenomena using quantitative techniques.", DateTime.Parse("2024-01-20"), DateTime.Parse("2024-02-19")),
                    ("Graphic Design", "Explores principles of visual communication, including graphic elements, layout design, and the use of images and typography to convey effective messages.", DateTime.Parse("2024-01-20"), DateTime.Parse("2024-02-19")),
                    ("Nutrition and Dietetics", "Covers the application of nutrition principles in clinical settings, addressing topics such as nutritional assessments, dietary interventions, and the role of nutrition in managing health conditions.", DateTime.Parse("2024-01-20"), DateTime.Parse("2024-02-19"))

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

                // (Email, Pw, FirstName, LastName, SosSecNo, PhoneNo, Address, Status, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality, CourseId
                var students = new (string, string, string, string, string, string, string, string, string, int, string, string, string, int)[]
                {
                    ("test1@test.se","P@sw0rd1","Eric","Larsson","770404-0099","070-46 46 506","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk", 1),
                    ("test2@test.se","P@sw0rd2","Stina","Pettersson","970404-4069","070-46 46 506","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk", 2),
                    ("test3@test.se", "P@sw0rd3", "Eric", "Larsson", "770404-0099", "070-46 46 506", "Sturegatan 12", "Går på kurs", "Jätte bra CV", 3, "Ducktig stunet", "Svenska", "Svensk", 3),
                    ("test4@test.se","P@sw0rd4","Eric","Larsson","770404-0099","070-46 46 506","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk", 4),
                    ("student1@home.se", "P@55word!", "Stefan", "Olsson", "123456789", "123456789", "Långa gatan 12", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("student2@home.se", "P@55word!", "Greta", "Sturesson", "123456790", "123456789", "Långa gatan 13", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("emil.andersson@home.se", "P@55word!", "Emil", "Andersson", "123456791", "123456789", "Långa gatan 14", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("viktor.johansson@home.se", "P@55word!", "Viktor", "Johansson", "123456792", "123456789", "Långa gatan 15", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("h.gustafsson@home.se", "P@55word!", "Sofia", "Gustafsson", "123456793", "123456789", "Långa gatan 16", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("blomman.b@home.se", "P@55word!", "Axel", "Bergqvist", "123456794", "123456789", "Långa gatan 17", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("jaggillarkatter@home.se", "P@55word!", "Isabella", "Eriksson", "123456795", "123456789", "Långa gatan 18", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("oscar.s@home.se", "P@55word!", "Oscar", "Svensson", "123456796", "123456789", "Långa gatan 19", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("clara.horses@home.se", "P@55word!", "Clara", "Larsson", "123456797", "123456789", "Långa gatan 20", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("stella.eri@home.se", "P@55word!", "Stella", "Eriksson", "123456798", "123456789", "Långa gatan 21", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("novalovasvensson@home.se", "P@55word!", "Nova", "Svensson", "123456799", "123456789", "Långa gatan 22", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("eliasnilsson@home.se", "P@55word!", "Elias", "Nilsson", "123456800", "123456789", "Långa gatan 23", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("lurre@home.se", "P@55word!", "Lucas", "Lundgren", "123456801", "123456789", "Långa gatan 24", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("emilia1982@home.se", "P@55word!", "Emilia", "Dahlström", "123456802", "123456789", "Långa gatan 25", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("majaisthebest@home.se", "P@55word!", "Maja", "Karlsson", "123456803", "123456789" , "Långa gatan 26", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("liamlarsson@home.se", "P@55word!", "Liam", "Larsson", "123456804", "123456789" , "Långa gatan 27", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("linnea.persson@home.se", "P@55word!", "Linnea", "Persson", "123456805", "123456789" , "Långa gatan 28", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("sebastian_andersson@home.se", "P@55word!", "Sebastian", "Andersson", "123456806","123456789", "Långa gatan 29", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("hugo.l@home.se", "P@55word!", "Hugo", "Lindqvist", "123456807", "123456789", "Långa gatan 30", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("a.gustafsson@home.se", "P@55word!", "Alva", "Gustafsson", "123456808","123456789", "Långa gatan 31", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("superwilma@home.se", "P@55word!", "Wilma", "Bergström", "123456809", "123456789" , "Långa gatan 32", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("klarafardigaga@home.se", "P@55word!", "Klara", "Jansson", "123456810", "123456789" , "Långa gatan 33", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("stellaostman@home.se", "P@55word!", "Stella", "Östman", "123456811",  "123456789" , "Långa gatan 34", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("thea.b@home.se", "P@55word!", "Thea", "Berggren", "123456812", "123456789" , "Långa gatan 35", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("gabbe1990@home.se", "P@55word!", "Gabriel", "Svensson", "123456813", "123456789" , "Långa gatan 36", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("elin.lundin@home.se", "P@55word!", "Elin", "Lundin", "123456814", "123456789" , "Långa gatan 37", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("benji.falken@home.se", "P@55word!", "Benjamin", "Falk", "123456815", "123456789" , "Långa gatan 38", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("joel.lindell@home.se", "P@55word!", "Joel", "Lindell", "123456816", "123456789" , "Långa gatan 39", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("bara.vara.vera@home.se", "P@55word!", "Vera", "Gustavsson", "123456817", "123456789" , "Långa gatan 40", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("olivia.b@home.se", "P@55word!", "Olivia", "Björk", "123456818", "123456789" , "Långa gatan 41", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("erik_karlberg@home.se", "P@55word!", "Erik", "Karlberg", "123456819", "123456789" , "Långa gatan 42", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("simon_n@home.se", "P@55word!", "Simon", "Nyström", "123456820", "123456789" , "Långa gatan 43", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("aliceiunderlandet1980@home.se", "P@55word!", "Alice", "Nilsson", "123456821", "123456789" , "Långa gatan 44", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("ella_slangbella@home.se", "P@55word!", "Ella", "Andersson", "123456822", "123456789" , "Långa gatan 45", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("noah.eklund@home.se", "P@55word!", "Noah", "Eklund", "123456823", "123456789" , "Långa gatan 46", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("anton_persson@home.se", "P@55word!", "Anton", "Persson", "123456824", "123456789" , "Långa gatan 47", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 ),
                    ("linneadahlberg@home.se", "P@55word!", "Linnea", "Dahlberg", "123456825", "123456789" , "Långa gatan 48", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2 ),
                    ("isak.b@home.se", "P@55word!", "Isak", "Bergman", "123456826", "123456789" , "Långa gatan 49", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3 ),
                    ("viktigaviktor@home.se", "P@55word!", "Viktor", "Holmström", "123456827", "123456789" , "Långa gatan 50", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4 ),
                    ("oliver.oberg@home.se", "P@55word!", "Oliver", "Öberg", "123456828", "123456789" , "Långa gatan 51", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5 ),
                    ("anton_ericsson@home.se", "P@55word!", "Anton ", "Ericsson", "123456829", "123456789" , "Långa gatan 52", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1 )
                };
                await AddStudentsAsync(students);
            }
            //#################################################################################


            //##-< Seed Companies data >-######################################################
            if (!await db.Companies.AnyAsync())
            {
                // (CompanyName, OrganizationNumber, Website, Email, Phone, PAdress, PNo, Citty, Notes)
                var company = new (string, string, string, string, string, string, string, string, string)[]
                {
                    ("Alfalaval", "555555-5555", "www.alfalaval.se", "info@alfalaval.se", "08-555 555 55", "Separator vägen 1", "123 45", "Tumba", "Tillverkar mjölkningsmaskiner och separatorer för mejeriindustrin."),
                    ("Min dåliga fantasi", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi."),
                    ("Mer dålig fantasi", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi."),
                    ("Änny mer dålig fantasi", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi."),
                    ("Nu tog den slut", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi.")
                };
                await AddCompaniesAsync(company);
            }
            //#################################################################################


            //##-< Seed project data >-########################################################
            if (!await db.Projects.AnyAsync())
            {
                // (Description, NoOfInterns, DefaultStartDate, DefaultEndDate, CompanyID)
                var projects = new(string, int,DateTime, DateTime, int)[]
                { 
                    ("Utveckla ett kursbokningssystem. Använda C#, .NET, och Blazer. Meriterande med databas kunskaper.", 5, DateTime.Parse("2024-04-04"), DateTime.Parse("2024-08-04"), 1),
                    ("Utveckla ett kursbokningssystem. Använda C#, .NET, och Blazer. Meriterande med databas kunskaper.", 5, DateTime.Parse("2024-04-04"), DateTime.Parse("2024-08-04"), 2)
                };
                await AddProjectsAsync(projects);
            }
            //#################################################################################
        }
        //#####################################################################################



        //##-< Seed Courses Method >-##########################################################
        private static async Task AddCoursesAsync((string, string, DateTime, DateTime)[] _courses)
        {
            string name, description; DateTime startDate, endDate;

            foreach (var _course in _courses)
            {
                (name, description, startDate, endDate) = _course;
                var newCourse = new Course
                {
                    Name = name,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate
                };
                await db.Courses.AddAsync(newCourse);
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
        private static async Task AddStudentsAsync((string, string, string, string, string, string, string, string, string, int, string, string, string, int)[] students)
        {
            string email, pw, firstName, lastName, sosSecNo, phoneNo, address, status, cV, commentByTeacher, language, nationality; int knowledgeLevel, courseId;
            foreach (var student in students)
            {
                // (Email, Pw, FirstName, LastName, SosSecNo, PhoneNo, Address, Status, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality, CourseId
                (email, pw, firstName, lastName, sosSecNo, phoneNo, address, status, cV, knowledgeLevel, commentByTeacher, language, nationality, courseId) = student;
                var newStudent = new ApplicationUser
                {
                    IsStudent = true,
                    UserName = email.Trim(),
                    Email = email.ToLower().Trim(),
                    EmailConfirmed = true,
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    StudentSocSecNo = sosSecNo.Trim(),
                    PhoneNumber = phoneNo.Trim(),
                    Address = address.Trim(),
                    Status = status.Trim(),
                    CV = cV.Trim(),
                    KnowledgeLevel = knowledgeLevel,
                    CommentByTeacher = commentByTeacher.Trim(),
                    Language = language.Trim(),
                    Nationality = nationality.Trim()
                };
                var result = await userManager.CreateAsync(newStudent, pw);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

                await AddUserToRoleAsync(newStudent, "Student");
                await AddStudentToCourseAsync(newStudent.Id, courseId);
            }
            await db.SaveChangesAsync();
        }
        //#####################################################################################



        //##-< Method to enroll a Student to a Cours >-########################################
        private static async Task AddStudentToCourseAsync(string id, int courseId)
        {
            var enrollment = new Enrollment
            {
                ApplicationUserId = id,
                CourseId = courseId,
            };
            await db.Enrollments.AddAsync(enrollment);
        }
        //#####################################################################################



        //##-< Method to connect a User to a Role >-###########################################
        private static async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }
        //#####################################################################################


        //##-< Method to add companies >-######################################################
        private static async Task AddCompaniesAsync((string, string, string, string, string, string, string, string, string)[] companies)
        {
            // (CompanyName, OrganizationNumber, Website, Email, Phone, PAdress, PNo, Citty, Notes)
            string companyName, organizationNumber, website, companyEmail, phone, postalAdress, postalNumber, city, notes;
            foreach (var company in companies)
            {
                (companyName, organizationNumber, website, companyEmail, phone, postalAdress, postalNumber, city, notes) = company;
                var newCompany = new Company
                {
                    CompanyName = companyName,
                    OrganizationNumber = organizationNumber,
                    Website = website,
                    CompanyEmail = companyEmail,
                    Phone = phone,
                    PostalAdress = postalAdress,
                    PostalNumber = postalNumber,
                    City = city,
                    Notes = notes
                };
                await db.Companies.AddAsync(newCompany);
            }
            await db.SaveChangesAsync();
        }
        //#####################################################################################



        //##-< Seed Project Method >-##########################################################
        private static async Task AddProjectsAsync((string, int, DateTime, DateTime, int)[] projects)
        {
            // (Description, NoOfInterns, DefaultStartDate, DefaultEndDate, CompanyID)

            string description; int noOfStudents, companyID; DateTime defaultStartDate, defaultEndDate;
            foreach (var project in projects)
            { 
                (description, noOfStudents, defaultStartDate, defaultEndDate, companyID) = project;
                var newProject = new Project
                { 
                    ProjectDescription = description,
                    DefaultStartDate = defaultStartDate,
                    DefaultEndDate = defaultEndDate,
                    CompanyId = companyID
                };
                await db.Projects.AddAsync(newProject);
            }
            await db.SaveChangesAsync();
        }
        //#####################################################################################


        //##-< ???????????? >-#################################################################
        // New methods goes here.
        //#####################################################################################
    }
}
