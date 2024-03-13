using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class CourseList
    {
        [Parameter]
        public IEnumerable<CourseForShortListDTO> CompListOfCourses { get; set; } = new List<CourseForShortListDTO>();
    }
}