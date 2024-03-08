using Microsoft.AspNetCore.Components;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Client.Components
{
    public partial class StudentList : ComponentBase
    {
        [Parameter]
        public  IEnumerable<StudentForListDTO> Students { get; set; } = new List<StudentForListDTO>();
    }
}
