using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.CoursesDTOs
{
    public class ListOfCoursesDTO
    {
        public List<CourseForShortListDTO> ListOfCourses { get; set; } = new List<CourseForShortListDTO>();

        //public ListOfCoursesDTO()
        //{
        //    ListOfCourses.Add(new CourseForShortListDTO { Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
        //    ListOfCourses.Add(new CourseForShortListDTO { Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
        //    ListOfCourses.Add(new CourseForShortListDTO { Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
        //    ListOfCourses.Add(new CourseForShortListDTO { Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
        //}
    }
}
