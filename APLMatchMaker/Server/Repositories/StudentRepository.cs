using APLMatchMaker.Server.Data;
using APLMatchMaker.Server.Models;
using APLMatchMaker.Server.ResourceParameters;
using APLMatchMaker.Server.Helpers;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using APLMatchMaker.Server.Models.Entities;

namespace APLMatchMaker.Server.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        //##-< Properties >-###############################################################
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager = default!;
        private readonly IPropertyMappingService _propertyMappingService;
        //#################################################################################


        //##-< Constructor >-##############################################################
        public StudentRepository(ApplicationDbContext dbContext, IServiceProvider services, IPropertyMappingService propertyMappingService)
        {
            _db = dbContext;
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            _propertyMappingService = propertyMappingService;
        }
        //#################################################################################


        //##-< Search or filter students as list >-########################################
        public async Task<PagedList<ApplicationUser>> GetAsync(StudentResourceParameters? studentResourceParameters)
        {
            if (studentResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(studentResourceParameters));
            }

            // Student collection to start from.
            var studentCollection = _db.ApplicationUsers.Where(sc => sc.IsStudent == true) as IQueryable<ApplicationUser>;

            // Filter on specific properties/db-fields.
            if (!string.IsNullOrWhiteSpace(studentResourceParameters.Address))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.Address!.Contains(studentResourceParameters.Address.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(studentResourceParameters.Status))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.StatusOther!.Contains(studentResourceParameters.Status.Trim()));
            }

            if (studentResourceParameters.KnowledgeLevel.HasValue)
            {
                studentCollection = studentCollection.Where(sc =>
                sc.KnowledgeLevel == studentResourceParameters.KnowledgeLevel);
            }

            if (studentResourceParameters.CVIntro.HasValue)
            {
                studentCollection = studentCollection.Where(sc =>
                sc.CVIntro == studentResourceParameters.CVIntro);
            }

            if (studentResourceParameters.LinkedinIntro.HasValue)
            {
                studentCollection = studentCollection.Where(sc =>
                sc.LinkedinIntro == studentResourceParameters.LinkedinIntro);
            }

            if (studentResourceParameters.Workshopdag.HasValue)
            {
                studentCollection = studentCollection.Where(sc =>
                sc.Workshopdag == studentResourceParameters.Workshopdag);
            }

            if (studentResourceParameters.APLSamtal.HasValue)
            {
                studentCollection = studentCollection.Where(sc =>
                sc.APLSamtal == studentResourceParameters.APLSamtal);
            }

            if (!string.IsNullOrWhiteSpace(studentResourceParameters.Language))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.Language!.Contains(studentResourceParameters.Language.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(studentResourceParameters.Nationality))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.Nationality!.Contains(studentResourceParameters.Nationality.Trim()));
            }

            // Search on a group of properties/db-fields.
            if (!string.IsNullOrWhiteSpace(studentResourceParameters.SearchQuery))
            {
                studentCollection = studentCollection.Where(sc =>
                sc.FirstName.Contains(studentResourceParameters.SearchQuery.Trim()) ||
                sc.LastName.Contains(studentResourceParameters.SearchQuery.Trim()) ||
                sc.Email!.Contains(studentResourceParameters.SearchQuery.Trim()) ||
                sc.Address!.Contains(studentResourceParameters.SearchQuery.Trim()) ||
                sc.CommentByTeacher!.Contains(studentResourceParameters.SearchQuery.Trim()) ||
                sc.Language!.Contains(studentResourceParameters.SearchQuery.Trim()) ||
                sc.Nationality!.Contains(studentResourceParameters.SearchQuery.Trim()) ||
                sc.Miscellaneous!.Contains(studentResourceParameters.SearchQuery.Trim())
                );
            }

            // Sort/arrange rows/records in specified order.
            if (!string.IsNullOrWhiteSpace (studentResourceParameters.OrderBy)) 
            {
                var studentPropertyMappingDictionary = _propertyMappingService
                    .GetPropertyMapping<StudentForListDTO, ApplicationUser>();

                studentCollection = studentCollection.ApplySort(studentResourceParameters.OrderBy,
                    studentPropertyMappingDictionary);
            }

            return await PagedList<ApplicationUser>.CreateAsync(studentCollection,
                studentResourceParameters.PageNumber, studentResourceParameters.PageSize);
        }
        //#################################################################################


        //##-< Get one student, by id >-###################################################
        public async Task<ApplicationUser?> GetAsync(Guid id)
        {
            return await _db.ApplicationUsers.Where(au => au.Id == id.ToString() && au.IsStudent == true)
                .Include(au => au.Course!).ThenInclude(en => en.Course)
                .Include(au => au.Internships!).ThenInclude(i => i.Project!).ThenInclude(pr => pr.Company)
                .Include(au => au.Company)
                .FirstOrDefaultAsync();
        }
        //#################################################################################


        //##-< Add one new student >-######################################################
        public async Task<bool> AddAsync(ApplicationUser _applicationUser, string password)
        {
            try
            {
                await _userManager.CreateAsync(_applicationUser, _db.DefaultPw);
                await _userManager.AddToRoleAsync(_applicationUser, "Student");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //#################################################################################


        //##-< Update student >-#############################################################
        public bool UpdateStudent(ApplicationUser studentToUpdate)
        {
            try
            {
                _db.ApplicationUsers.Update(studentToUpdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //#################################################################################


        //##-< Delete one existing student >-##############################################
        public async Task<bool> RemoveAsync(Guid _Id)
        {
            var _au = await _db.ApplicationUsers.FindAsync(_Id.ToString());

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


        //##-< EmailExist >-###############################################################
        public async Task<bool> EmailExistAsync(string email)
        {
            return (bool)await _db.ApplicationUsers.Where(au => au.Email == email.ToLower().Trim()).AnyAsync();
        }
        //#################################################################################


        //##-< Has engagements >-##########################################################
        public async Task<bool> HasEngagementsAsync(Guid id)
        {
            var student = await _db.ApplicationUsers.Where(au => au.Id == id.ToString())
                .Include(au => au.Course)
                .Include(au => au.Internships)
                .Include(au => au.Company)
                .FirstOrDefaultAsync();
            if (student == null)
            {
                return false;
            }
            return (student.Course!.Count() > 0) ||
                (student.Internships!.Count() > 0) ||
                (student.Company != null);
        }
        //#################################################################################


        //##-< ???????????? >-#############################################################
        // New methods goes here.
        //#################################################################################
    }
}
