using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.ResourceParameters;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APLMatchMaker.Server.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public static UserManager<ApplicationUser> _userManager = default!;


        //##-< Constructor >-#############################################################
        public StudentRepository(ApplicationDbContext dbContext, IServiceProvider services)
        {
            _db = dbContext;
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        }
        //#################################################################################


        //##-< Get all students as list >-#################################################
        public async Task<List<ApplicationUser>> GetAsync()
        {
            return await _db.ApplicationUsers.Where(au => au.IsStudent == true).ToListAsync();

        }
        //#################################################################################


        //##-< Search or filter students as list >-########################################
        public async Task<List<ApplicationUser>> GetAsync(StudentResourceParameters? studentResourceParameters)
        {
            if (studentResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(studentResourceParameters));
            }

            if (string.IsNullOrWhiteSpace(studentResourceParameters.FullName) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.UserName) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.Email) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.PhoneNumber) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.StudentSocSecNo) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.Address) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.Status) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.CV) &&
                !studentResourceParameters.KnowledgeLevel.HasValue &&
                !studentResourceParameters.CVIntro.HasValue &&
                !studentResourceParameters.LinkedinIntro.HasValue &&
                !studentResourceParameters.Workshopdag.HasValue &&
                !studentResourceParameters.APLSamtal.HasValue &&
                string.IsNullOrWhiteSpace(studentResourceParameters.Checklist) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.CommentByTeacher) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.Language) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.Nationality) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.Miscellaneous) &&
                string.IsNullOrWhiteSpace(studentResourceParameters.SearchQuery))
            {
                return await GetAsync();
            }

            // Student collection to start from.
            var studentCollection = _db.ApplicationUsers.Where(sc => sc.IsStudent == true) as IQueryable<ApplicationUser>;

            if (!string.IsNullOrWhiteSpace(studentResourceParameters.FullName))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.FirstName.Contains(studentResourceParameters.FullName.Trim()) ||
                sc.LastName.Contains(studentResourceParameters.FullName.Trim()));
            }
            
            if (!string.IsNullOrWhiteSpace(studentResourceParameters.UserName))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.UserName!.Contains(studentResourceParameters.UserName.Trim()));
            }
            
            if (!string.IsNullOrWhiteSpace(studentResourceParameters.Email))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.Email!.Contains(studentResourceParameters.Email.Trim()));
            }
            
            if (!string.IsNullOrWhiteSpace(studentResourceParameters.PhoneNumber))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.PhoneNumber!.Contains(studentResourceParameters.PhoneNumber.Trim()));
            }

            return await studentCollection.ToListAsync();
        }
        //#################################################################################


        //##-< Get one student, by id >-###################################################
        public async Task<ApplicationUser?> GetAsync(string id)
        {
            return await _db.ApplicationUsers.FirstOrDefaultAsync(au => au.Id == id && au.IsStudent == true);
        }
        //#################################################################################


        //##-< Add one new student >-######################################################
        public async Task<bool> AddAsync(ApplicationUser _applicationUser, string password)
        {
            try
            {
                await _userManager.CreateAsync(_applicationUser, password);
                await _userManager.AddToRoleAsync(_applicationUser, "Student");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //#################################################################################


        //##-< Update one existing student >-##############################################
        public void Update(ApplicationUser _applicationUser)
        {
            //_db.ApplicationUsers.Update(_applicationUser);
        }
        //#################################################################################


        //##-< Delete one existing student >-##############################################
        public async Task<bool> RemoveAsync(string _Id)
        {
            var _au = await _db.ApplicationUsers.FindAsync(_Id);

            if (_au == null)
            {
                return false;
            }

            try
            {
                _db.ApplicationUsers.Remove(_au);
                await CompleteAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        //#################################################################################


        //##-< Save changes >-#############################################################
        public async Task<bool> CompleteAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //#################################################################################


        //##-< ???????????? >-#############################################################
        // New methods goes here.
        //#################################################################################
    }
}
