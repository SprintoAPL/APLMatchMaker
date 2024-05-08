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
                    //("Alfalaval", "555555-5555", "www.alfalaval.se", "info@alfalaval.se", "08-555 555 55", "Separator vägen 1", "123 45", "Tumba", "Tillverkar mjölkningsmaskiner och separatorer för mejeriindustrin."),
                    //("Min dåliga fantasi", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi."),
                    //("Mer dålig fantasi", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi."),
                    //("Änny mer dålig fantasi", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi."),
                    //("Nu tog den slut", "555555-5555", "www.xxx.se", "info@test.se", "08-555 555 55", "Skogsstige 1465416", "123 45", "Farsta", "Så här långt räcker min fantasi.")
                    ("Alfalaval", "7727-8755", "www.alfalaval.se", "info@alfalaval.se", "08-533 664 55", "Separator vägen 1", "123 45", "Tumba", "Tillverkar mjölkningsmaskiner och separatorer för mejeriindustrin."),
                    ("Lexicon", "5655-1255", "www.Lexicon.se", "info@Lexi.se", "08-755 12 55", "Skogstigrn 57", "157 45", "Farsta", "Programvaru utvecklare för stora företag."),
                    ("Banan AB", "2541-6564", "www.Google.se", "info@Gmail.se", "08-555 787 22", "Sveavägen 25", "123 88", "Stockholm", "Importerar bananer från Gotland."),
                    ("Alkemist", "7071-1170", "www.Alkemist.se", "info@alkemist.se", "08-211 122 55", "Valhallavägen 101", "121 18", "Stockholm", "En av världens största kemi företag."),
                    ("SpA AB", "2235-3211", "www.spa.se", "info@spa.se", "08-152 1 21", "Spångagatan 27", "522 21", "Göteborg", "En till stort sport företag känt över hela världen."),
                    ("Sportia AB", "2245-3211", "www.sportia.se", "info@sportia.se", "08-111 221 21", "Arenavägen 201", "103 21", "Göteborg", "Ett stor sport företag känt över hela världen."),
                    ("ABCEX AB", "2785-3211", "www.Par.se", "info@par.se", "08-11 221 21", "Arenavägen 201", "103 21", "Göteborg", "Ett stor sport företag känt över hela världen."),
                    ("DCT AB", "2785-3211", "www.Par.se", "info@par.se", "08-11 221 21", "Arenavägen 201", "103 21", "Göteborg", "Ett stor sport företag känt över hela världen."),
                    ("TYG AB", "2785-3211", "www.Par.se", "info@par.se", "08-11 221 21", "Arenavägen 201", "103 21", "Göteborg", "Ett stor sport företag känt över hela världen.")
                };
                await AddCompaniesAsync(company);
            }
            //#################################################################################


            //##-< Seed project data >-########################################################
            if (!await db.Projects.AnyAsync())
            {
                // (Name, Description, NoOfInterns, DefaultStartDate, DefaultEndDate, CompanyID)
                var projects = new (string, string, int, DateTime, DateTime, int)[]
                {
                    ("Kursbokning","Utveckla ett kursbokningssystem.\nAnvända C#, .NET, och Blazer.\nMeriterande med databas kunskaper.", 5, DateTime.Parse("2024-04-04"), DateTime.Parse("2024-08-04"), 1),
                    ("Kursbokning","Utveckla ett kursbokningssystem.\nAnvända C#, .NET, och Blazer.\nMeriterande med databas kunskaper.", 5, DateTime.Parse("2024-04-04"), DateTime.Parse("2024-08-04"), 2)
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

                // (Email, FirstName, LastName, SosSecNo, PhoneNo, Address, StatusSelect, StatusWhen, StatusOther, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality, CourseId, ProjectId, CompanyId)
                var students = new (string, string, string, string, string, string, int, DateTime?, string, string, int, string, string, string, int?, int?, int?)[]
                {
                    ("test1@test.se", "Eric","Larsson","780204-1099","070-46 46 506","Stockholm",1,null,"Går på kurs","Jätte bra CV", 3, "Ducktig stunet","Svenska, engelska","Svensk", 1, null, null),
                    ("test2@test.se", "Stina","Pettersson","970404-4069","072-28 72 753","Hjo",1,null,"Går på kurs","Jätte bra CV", 2, "Ducktig stunet","Svenska","Svensk", 2, null, null),
                    ("test3@test.se",  "Eric", "Larsson", "770404-0099", "070-60 07 448", "Uppsala", 1,null,"Går på kurs", "Jätte bra CV", 3, "Ducktig stunet", "Svenska, engelska", "Svensk", 3, null , null),
                    ("test4@test.se", "Eric", "Larsson", "870404-1194", "073-66 52 459", "Göteborg", 1, null, "Går på kurs", "Jätte bra CV", 3, "Ducktig stunet", "Svenska, engelska, franska", "Svensk", 4, null, null),
                    ("student1@home.se", "Stefan", "Olsson", "830408-2061", "071-48 02 083", "Stockholm", 1, null, "Går på kurs", "Jättebra CV", 1, "Duktig elev", "Svenska, spanska, engelska", "Svensk", 1, null, null),
                    ("student2@home.se", "Greta", "Sturesson", "050625-1263", "073-92 00 952", "Karlstad", 1, null, "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, spanska", "Svensk", 2, null, null),
                    ("emil.andersson@home.se", "Emil", "Andersson", "650725-6250", "072-56 88 043", "Linköping", 1, null, "Går på kurs", "Jättebra CV", 2, "Duktig elev", "Svenska, engelska", "Svensk", 3, null, null),
                    ("viktor.johansson@home.se", "Viktor", "Johansson", "070326-2253", "070-04 91 797", "Södertälje", 1, null, "Går på kurs", "Jättebra CV", 1, "Duktig elev", "Svenska, engelska", "Svensk", 4, null, null),
                    ("h.gustafsson@home.se", "Sofia", "Gustafsson", "020807-2637", "073-26 77 776", "Uppsala", 1, null, "Går på kurs", "Jättebra CV", 1, "Duktig elev", "Svenska, engelska", "Svensk", 5, null, null),
                    ("blomman.b@home.se", "Axel", "Bergqvist", "991115-7999", "075-87 86 594", "Göteborg", 1, null, "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 1, null, null),
                    ("jaggillarkatter@home.se", "Isabella", "Novell", "040701-1337", "074-48 64 033", "Lerum", 1, null, "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, franska", "Fransk", 2, null, null),
                    ("oscar.s@home.se", "Oscar", "Svensson", "040328-7030", "071-55 93 273", "Trollhättan", 1, null, "Går på kurs", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, tyska", "Svensk", 3, null, null),
                    ("clara.horses@home.se", "Clara", "Larsson", "820910-9969", "072-64 15 910", "Karlstad", 2,null,"Har praktik", "Jättebra CV", 2, "Duktig elev", "Svenska", "Svensk", 4, 1, null),
                    ("stella.eri@home.se",  "Stella", "Eriksson", "010409-7878", "072-64 12 701", "Vänersborg", 2,null,"Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, spanska", "Svensk", 5, 2, null),
                    ("novalovasvensson@home.se", "Nova", "Svensson", "720727-6341", "072-64 12 702", "Göteborg", 2,null,"Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 1, 1, null),
                    ("eliasnilsson@home.se", "Elias", "Nilsson", "950517-1968", "070-17 40 631", "Stockholm", 2,null,"Har praktik", "Jättebra CV", 2, "Duktig elev", "Svenska, engelska", "Svensk", 2, 2, null),
                    ("lurre@home.se",  "Lucas", "Lundgren", "870527-7005", "070-17 40 622", "Sigtuna", 2,null,"Har praktik", "Jättebra CV", 2, "Duktig elev", "Svenska, engelska", "Svensk", 3, 1, null),
                    ("emilia1982@home.se", "Emilia", "Dahlström", "810717-2879", "070-17 40 612", "Uppsala", 2,null,"Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 4, 1, null),
                    ("majaisthebest@home.se","Maja", "Schmacke", "890301-5710", "070-17 40 605", "Örebro", 2,null,"Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, tyska", "Tysk", 5, 2, null),
                    ("liamlarsson@home.se", "Liam", "Larsson", "940425-7686", "070-17 40 610", "Uddevalla", 2,null,"Har praktik", "Jättebra CV", 2, "Duktig elev", "Svenska, engelska", "Svensk", 1, 1, null),
                    ("linnea.persson@home.se","Linnea", "Persson", "001012-1556", "073-17 40 627", "Göteborg", 2,null,"Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 2, 2, null),
                    ("sebastian_andersson@home.se", "Sebastian", "Andersson", "071118-8714", "071-17 40 620", "Strömstad", 2,null,"Har praktik", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 3, 1, null),
                    ("hugo.l@home.se", "Hugo", "Lindqvist", "960602-4645", "071-17 40 615", "Karlskoga", 2,null,"Har praktik", "Jättebra CV", 2, "Duktig elev", "Svenska, engelska", "Svensk", 4, 2, null),
                    ("a.gustafsson@home.se", "Alva", "Gustafsson", "780821-2745", "071-17 40 614", "Örebro", 4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 2, "Duktig elev", "Svenska, engelska, franska", "Svensk", 5, null, 1),
                    ("superwilma@home.se", "Wilma", "Bergström", "870708-4680", "070-17 40 615", "Stockholm",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 1, "Duktig elev", "Svenska, engelska, tyska", "Svensk", 1, null, 1),
                    ("klarafardigaga@home.se", "Klara", "Jansson", "030307-4397", "070-17 40 608", "Västerås",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", null, 1, 2),
                    ("stellaostman@home.se",  "Stella", "Östman", "991107-7247", "073-17 40 623", "Uppsala",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, tyska, italienska", "Svensk", 3, null, 3),
                    ("thea.b@home.se", "Thea", "Berggren", "040220-2139", "074-17 40 610", "Vänersborg",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, tyska", "Svensk", 4, null, 4),
                    ("gabbe1990@home.se", "Gabriel", "Svensson", "830218-6674", "070-17 40 612", "Göteborg",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 5, null, 4),
                    ("elin.lundin@home.se", "Elin", "Lundin", "921001-2986", "070-17 40 630", "Södertälje",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 1, null, 1),
                    ("benji.falken@home.se", "Benjamin", "Falk", "750322-6883", "070-17 40 613", "Märsta",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 2, "Duktig elev", "Svenska, engelska, tyska", "Svensk", 2, null, 2),
                    ("joel.lindell@home.se", "Joel", "Lindell", "061228-7037", "070-17 40 606", "Nynäshamn",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 3, null, 3),
                    ("bara.vara.vera@home.se","Vera", "Gustavsson", "061228-7037", "070-17 40 610", "Stockholm",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska", "Svensk", 4, null, 4),
                    ("olivia.b@home.se", "Olivia", "Björk", "870710-6269", "070-17 40 631", "Hjo",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, engelska, franska", "Svensk", 5, 1, 5),
                    ("erik_karlberg@home.se", "Erik", "Karlberg", "031130-3580", "070-17 40 624", "Skara",  4,DateTime.Parse("2024-05-05"), "Har fått jobb", "Jättebra CV", 3, "Duktig elev", "Svenska, tyska, engelska", "Tysk", 1, 2, 1),
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
                // (Email, FirstName, LastName, PhoneNo, Address, Title, CompanyId)
                var contacts = new (string, string, string, string, string, string, int?)[]
                {
                    ("simon_n@home.se", "Simon", "Nyström",  "010-71 86 84", "Stockholm", "Byrådirektör", 1),
                    ("aliceiunderlandet1980@home.se", "Alice", "Nilsson", "070-01 26 536", "Märsta", "HR-ansvarig", 2),
                    ("ella_slangbella@home.se", "Ella", "Andersson", "010-19 50 27", "Stockholm", "VD", 3),
                    ("noah.eklund@home.se", "Noah", "Eklund",  "071-39 12 591", "Göteborg", "Team leader", 4),
                    ("anton_persson@home.se",  "Anton", "Persson",  "010-59 58 60", "Trollhättan", "Rekryterare", 5),
                    ("linneadahlberg@home.se", "Linnea", "Dahlberg", "076-61 21 629", "Göteborg", "Byrådirektör", 1),
                    ("isak.b@home.se", "Isak", "Bergman", "070-92 20 427", "Karlstad", "HR-ansvarig", 2),
                    ("viktigaviktor@home.se", "Viktor", "Holmström",  "010-45 99 35", "Södertälje", "Byrådirektör", 3),
                    ("oliver.oberg@home.se",  "Oliver", "Öberg",  "073-90 13 226", "Uppsala", "VD", 4),
                    ("anton_ericsson@home.se", "Anton ", "Ericsson", "071-41 06 251", "Södertälje", "Projektledare", 5)
                };
                await AddContactsAssync(contacts);
            }
            //#################################################################################
        }
        //#####################################################################################



        //##-< Seed Contacts Method >-##########################################################
        private static async Task AddContactsAssync((string, string, string, string, string, string, int?)[] contacts)
        {
            // (Email, Pw, FirstName, LastName, PhoneNo, Address, Title,)
            string email, firstName, lastName, phoneNo, address, title; int? companyId;
            foreach (var contact in contacts)
            {
                (email, firstName, lastName, phoneNo, address, title, companyId) = contact;
                var newContact = new ApplicationUser
                {
                    IsCompanyContact = true,
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNo,
                    Address = address,
                    Title = title,
                    CompanyId = companyId,
                };
                var result = await userManager.CreateAsync(newContact, db.DefaultPw);

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
        private static async Task AddStudentsAsync((string, string, string, string, string, string, int, DateTime?, string, string, int, string, string, string, int?, int?, int?)[] students)
        {
            string email, firstName, lastName, sosSecNo, phoneNo, address, statusOther, cV, commentByTeacher, language, nationality; int knowledgeLevel, statusSelect; int? courseId, projectId, companyId; DateTime? statusWhen;
            foreach (var student in students)
            {
                // (Email, FirstName, LastName, SosSecNo, PhoneNo, Address, StatusSelect, StatusWhen, StatusOther, CV, KnowledgeLevel, CommentByTeacher, Language, Nationality, CourseId
                (email, firstName, lastName, sosSecNo, phoneNo, address, statusSelect, statusWhen, statusOther, cV, knowledgeLevel, commentByTeacher, language, nationality, courseId, projectId, companyId) = student;
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
                    Status = statusSelect,
                    StatusWhen = statusWhen,
                    StatusOther = statusOther.Trim(),
                    CV = cV.Trim(),
                    KnowledgeLevel = knowledgeLevel,
                    CommentByTeacher = commentByTeacher.Trim(),
                    Language = language.Trim(),
                    Nationality = nationality.Trim(),
                    CompanyId = companyId,
                };
                var result = await userManager.CreateAsync(newStudent, db.DefaultPw);

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
        private static async Task AddProjectsAsync((string, string, int, DateTime, DateTime, int)[] projects)
        {
            // (Name, Description, NoOfInterns, DefaultStartDate, DefaultEndDate, CompanyID)

            string name, description; int noOfStudents, companyID; DateTime defaultStartDate, defaultEndDate;
            foreach (var project in projects)
            {
                (name, description, noOfStudents, defaultStartDate, defaultEndDate, companyID) = project;
                var newProject = new Project
                {
                    ProjectName = name,
                    ProjectDescription = description,
                    DefaultStartDate = defaultStartDate,
                    NumberOfInterns = noOfStudents,
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
