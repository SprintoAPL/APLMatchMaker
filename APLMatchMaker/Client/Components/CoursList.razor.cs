using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class CoursList
    {
        [Parameter]
        public IEnumerable<CoursForShortListDTO> CompListOfCourses { get; set; } = new List<CoursForShortListDTO>();
    }
}