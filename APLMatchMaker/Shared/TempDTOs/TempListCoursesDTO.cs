using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.TempDTOs
{
    public class TempListCoursesDTO
    {
        public List<TempCoursForListDTO> ListOfCourses { get; set; } = new List<TempCoursForListDTO>();
        public TempListCoursesDTO()
        {
            ListOfCourses.Add(new TempCoursForListDTO{ Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
            ListOfCourses.Add(new TempCoursForListDTO{ Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
            ListOfCourses.Add(new TempCoursForListDTO{ Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
            ListOfCourses.Add(new TempCoursForListDTO{ Id = 1, Name = "Some course", StartDate = DateTime.Parse("2024-02-29"), EndDate = DateTime.Parse("2024-05-30") });
        }
    }
}
