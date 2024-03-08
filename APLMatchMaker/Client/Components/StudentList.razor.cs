using Microsoft.AspNetCore.Components;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;

namespace APLMatchMaker.Client.Components
{
    public partial class StudentList : ComponentBase
    {
        [Parameter]
        public  IEnumerable<StudentForListDTO> Students { get; set; } = new List<StudentForListDTO>();
    }
}
