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
                var projects = new (string, int, DateTime, DateTime, int)[]
                {
                    ("Utveckla ett kursbokningssystem.\nAnvända C#, .NET, och Blazer.\nMeriterande med databas kunskaper.", 5, DateTime.Parse("2024-04-04"), DateTime.Parse("2024-08-04"), 1),
                    ("Utveckla ett kursbokningssystem.\nAnvända C#, .NET, och Blazer.\nMeriterande med databas kunskaper.", 5, DateTime.Parse("2024-04-04"), DateTime.Parse("2024-08-04"), 2)
                };
                await AddProjectsAsync(projects);
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

                // (Email, Pw, FirstName, LastName, SosSecNo, PhoneNo, Address, Status, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality, CourseId, ProjectId, CompanyId)
                var students = new (string, string, string, string, string, string, string, string, string, int, string, string, string, int?, int?, int?)[]
                {
                    ("test1@test.se","P@sw0rd1","Eric","Larsson","770404-0099","070-46 46 506","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk", 1, null, null),
                    ("test2@test.se","P@sw0rd2","Stina","Pettersson","970404-4069","070-46 46 506","Sturegatan 12","Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska","Svensk", 2, null, null),
                    ("test3@test.se", "P@sw0rd3", "Eric", "Larsson", "770404-0099", "070-46 46 506", "Sturegatan 12", "Går på kurs", "Jätte bra CV", 3, "Ducktig stunet", "Svenska", "Svensk", 3, null , null),
                    ("test4@test.se", "P@sw0rd4", "Eric", "Larsson", "770404-0099", "070-46 46 506", "Sturegatan 12", "Går på kurs", "Jätte bra CV", 3, "Ducktig stunet", "Svenska", "Svensk", 4, null , null),
                    ("student1@home.se", "P@55word!", "Stefan", "Olsson", "123456789", "123456789", "Långa gatan 12", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1, null, null),
                    ("student2@home.se", "P@55word!", "Greta", "Sturesson", "123456790", "123456789", "Långa gatan 13", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2, null , null),
                    ("emil.andersson@home.se", "P@55word!", "Emil", "Andersson", "123456791", "123456789", "Långa gatan 14", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3, null , null),
                    ("viktor.johansson@home.se", "P@55word!", "Viktor", "Johansson", "123456792", "123456789", "Långa gatan 15", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4, null , null),
                    ("h.gustafsson@home.se", "P@55word!", "Sofia", "Gustafsson", "123456793", "123456789", "Långa gatan 16", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5, null , null),
                    ("blomman.b@home.se", "P@55word!", "Axel", "Bergqvist", "123456794", "123456789", "Långa gatan 17", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1, null , null),
                    ("jaggillarkatter@home.se", "P@55word!", "Isabella", "Eriksson", "123456795", "123456789", "Långa gatan 18", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2, null , null),
                    ("oscar.s@home.se", "P@55word!", "Oscar", "Svensson", "123456796", "123456789", "Långa gatan 19", "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3, null , null),
                    ("clara.horses@home.se", "P@55word!", "Clara", "Larsson", "123456797", "123456789", "Långa gatan 20", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4, 1, null),
                    ("stella.eri@home.se", "P@55word!", "Stella", "Eriksson", "123456798", "123456789", "Långa gatan 21", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5, 2, null),
                    ("novalovasvensson@home.se", "P@55word!", "Nova", "Svensson", "123456799", "123456789", "Långa gatan 22", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1, 1, null),
                    ("eliasnilsson@home.se", "P@55word!", "Elias", "Nilsson", "123456800", "123456789", "Långa gatan 23", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2, 2, null),
                    ("lurre@home.se", "P@55word!", "Lucas", "Lundgren", "123456801", "123456789", "Långa gatan 24", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3, 1, null),
                    ("emilia1982@home.se", "P@55word!", "Emilia", "Dahlström", "123456802", "123456789", "Långa gatan 25", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4, 1, null),
                    ("majaisthebest@home.se", "P@55word!", "Maja", "Karlsson", "123456803", "123456789", "Långa gatan 26", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5, 2, null),
                    ("liamlarsson@home.se", "P@55word!", "Liam", "Larsson", "123456804", "123456789", "Långa gatan 27", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1, 1, null),
                    ("linnea.persson@home.se", "P@55word!", "Linnea", "Persson", "123456805", "123456789", "Långa gatan 28", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2, 2, null),
                    ("sebastian_andersson@home.se", "P@55word!", "Sebastian", "Andersson", "123456806", "123456789", "Långa gatan 29", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3, 1, null),
                    ("hugo.l@home.se", "P@55word!", "Hugo", "Lindqvist", "123456807", "123456789", "Långa gatan 30", "Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4, 2, null),
                    ("a.gustafsson@home.se", "P@55word!", "Alva", "Gustafsson", "123456808", "123456789", "Långa gatan 31", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5, null, 1),
                    ("superwilma@home.se", "P@55word!", "Wilma", "Bergström", "123456809", "123456789", "Långa gatan 32", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1, null, 1),
                    ("klarafardigaga@home.se", "P@55word!", "Klara", "Jansson", "123456810", "123456789", "Långa gatan 33", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", null, 1, 2),
                    ("stellaostman@home.se", "P@55word!", "Stella", "Östman", "123456811", "123456789", "Långa gatan 34", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3, null, 3),
                    ("thea.b@home.se", "P@55word!", "Thea", "Berggren", "123456812", "123456789", "Långa gatan 35", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4, null, 4),
                    ("gabbe1990@home.se", "P@55word!", "Gabriel", "Svensson", "123456813", "123456789", "Långa gatan 36", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5, null, 4),
                    ("elin.lundin@home.se", "P@55word!", "Elin", "Lundin", "123456814", "123456789", "Långa gatan 37", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1, null, 1),
                    ("benji.falken@home.se", "P@55word!", "Benjamin", "Falk", "123456815", "123456789", "Långa gatan 38", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 2, null, 2),
                    ("joel.lindell@home.se", "P@55word!", "Joel", "Lindell", "123456816", "123456789", "Långa gatan 39", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 3, null, 3),
                    ("bara.vara.vera@home.se", "P@55word!", "Vera", "Gustavsson", "123456817", "123456789", "Långa gatan 40", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 4, null, 4),
                    ("olivia.b@home.se", "P@55word!", "Olivia", "Björk", "123456818", "123456789", "Långa gatan 41", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 5, null, 5),
                    ("erik_karlberg@home.se", "P@55word!", "Erik", "Karlberg", "123456819", "123456789", "Långa gatan 42", "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska", "Svensk", 1, null, 1),
                };
                await AddStudentsAsync(students);
            }
            //#################################################################################


            //##-< Seed Company contacts data >-###############################################
            if (await db.ApplicationUsers.Where(au => au.IsCompanyContact == true).FirstOrDefaultAsync() == null)
            {
                //##-< Seed Contact Role >-####################################################
                if (!await roleManager.RoleExistsAsync("Contact"))
                {
                    var roleNames = new[] { "Contact" };
                    await AddRolesAsync(roleNames);
                }
                //#############################################################################
                // (Email, Pw, FirstName, LastName, PhoneNo, Address, Title, CompanyId)
                var contacts = new (string, string, string, string, string, string, string, int?)[]
                {
                    ("simon_n@home.se", "P@55word!", "Simon", "Nyström",  "123456789", "Långa gatan 43", "Byrådirektör", 1),
                    ("aliceiunderlandet1980@home.se", "P@55word!", "Alice", "Nilsson",  "123456789", "Långa gatan 44", "Byrådirektör", 2),
                    ("ella_slangbella@home.se", "P@55word!", "Ella", "Andersson", "123456789", "Långa gatan 45", "Byrådirektör", 3),
                    ("noah.eklund@home.se", "P@55word!", "Noah", "Eklund",  "123456789", "Långa gatan 46", "Byrådirektör", 4),
                    ("anton_persson@home.se", "P@55word!", "Anton", "Persson",  "123456789", "Långa gatan 47", "Byrådirektör", 5),
                    ("linneadahlberg@home.se", "P@55word!", "Linnea", "Dahlberg", "123456789", "Långa gatan 48", "Byrådirektör", 1),
                    ("isak.b@home.se", "P@55word!", "Isak", "Bergman", "123456789", "Långa gatan 49", "Byrådirektör", 2),
                    ("viktigaviktor@home.se", "P@55word!", "Viktor", "Holmström",  "123456789", "Långa gatan 50", "Byrådirektör", 3),
                    ("oliver.oberg@home.se", "P@55word!", "Oliver", "Öberg",  "123456789", "Långa gatan 51", "Byrådirektör", 4),
                    ("anton_ericsson@home.se", "P@55word!", "Anton ", "Ericsson", "123456789", "Långa gatan 52", "Byrådirektör", 5)
                };
                await AddContactsAssync(contacts);
            }
            //#################################################################################
        }
        //#####################################################################################



        //##-< Seed Contacts Method >-##########################################################
        private static async Task AddContactsAssync((string, string, string, string, string, string, string,int?)[] contacts)
        {
            // (Email, Pw, FirstName, LastName, PhoneNo, Address, Title,)
            string email, pw, firstName, lastName, phoneNo, address, title; int? companyId;
            foreach (var contact in contacts)
            {
                (email, pw, firstName, lastName, phoneNo, address, title, companyId) = contact;
                var newContact = new ApplicationUser
                {
                    IsCompanyContact = true,
                    UserName = email,
                    Email = pw,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNo,
                    Address = address,
                    Title = title,
                    CompanyId = null // companyId
                };
                var result = await userManager.CreateAsync(newContact, pw);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

                await AddUserToRoleAsync(newContact, "Contact");
            }
            await db.SaveChangesAsync();
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
        private static async Task AddStudentsAsync((string, string, string, string, string, string, string, string, string, int, string, string, string, int?, int?, int?)[] students)
        {
            string email, pw, firstName, lastName, sosSecNo, phoneNo, address, status, cV, commentByTeacher, language, nationality; int knowledgeLevel; int? courseId, projectId, companyId;
            foreach (var student in students)
            {
                // (Email, Pw, FirstName, LastName, SosSecNo, PhoneNo, Address, Status, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality, CourseId
                (email, pw, firstName, lastName, sosSecNo, phoneNo, address, status, cV, knowledgeLevel, commentByTeacher, language, nationality, courseId, projectId, companyId) = student;
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
                    Nationality = nationality.Trim(),
                    CompanyId = null // companyId
                };
                var result = await userManager.CreateAsync(newStudent, pw);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

                await AddUserToRoleAsync(newStudent, "Student");

                if (courseId != null && courseId > 0)
                {
                    await AddStudentToCourseAsync(newStudent.Id, (int)courseId);
                }

                if (projectId != null && projectId > 0)
                {
                    await AddStudentToProjectAsync(newStudent.Id, (int)projectId);
                }
            }
            await db.SaveChangesAsync();
        }
        //#####################################################################################



        //##-< Method to give a Student a Internship >-########################################
        private static async Task AddStudentToProjectAsync(string id, int projectId)
        {
            var internship = new Internship
            {
                ApplicationUserId = id,
                ProjectId = projectId,
            };
            await db.Internships.AddAsync(internship);
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
