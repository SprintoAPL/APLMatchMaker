using APLMatchMaker.Shared.TempDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class CoursList
    {
        [Parameter]
        public IEnumerable<TempCoursForListDTO> CompListOfCourses { get; set; } = new List<TempCoursForListDTO>();
    }
}