using APLMatchMaker.Server.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace APLMatchMaker.Server.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        //##-< Properties >-###############################################################
        private ApplicationDbContext _db;
        //#################################################################################


        //##-< Constructor >-##############################################################
        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        //#################################################################################


        //##-< Course exists >-############################################################
        public async Task<bool> CourseExistsAssynk(int couseId)
        {
            var course = await _db.Courses.FindAsync(couseId);
            return course != null;
        }
        //#################################################################################


        //##-< Student exists >-###########################################################
        public async Task<bool> StudentExistsAssync(Guid studentId, bool IsSudent)
        {
            var id = studentId.ToString();
            var student = IsSudent 
                ? await _db.ApplicationUsers.Where(au => au.Id.Equals(id) && au.IsStudent == true ).FirstOrDefaultAsync()
                : await _db.ApplicationUsers.FindAsync(id);
            return student != null;
        }
        //#################################################################################


        //##-< Remove student from course >-###############################################
        public async Task<bool> RemoveStudentFromCourse(int courseId, Guid studentId)
        {
            var stId = studentId.ToString();
            var enrolment = await _db.Enrollments.FindAsync(courseId, stId);
            if (enrolment == null)
            {
                return false;
            }
            _db.Enrollments.Remove(enrolment);
            await _db.SaveChangesAsync();
            return true;
        }
        //#################################################################################


        //##-< ???????????? >-#############################################################
        // New methods goes here.
        //#################################################################################

    }
}
