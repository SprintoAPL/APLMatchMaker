using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class StudentListTableBody
    {
        [Parameter]
        public IEnumerable<StudentForListDTO> Students { get; set; } = new List<StudentForListDTO>();
    }
}