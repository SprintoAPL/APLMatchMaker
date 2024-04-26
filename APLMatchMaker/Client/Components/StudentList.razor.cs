using Microsoft.AspNetCore.Components;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Client.Components
{
    public partial class StudentList : ComponentBase
    {
        [Parameter]
        public  IEnumerable<StudentForListDTO> Students { get; set; } = new List<StudentForListDTO>();

        private string sortBy = "columnTitle";
        private bool isAscending = true;

        private void SortByColumn(string columnTitle)
        {
            if (sortBy == columnTitle)
            {
                isAscending = !isAscending;
            }
            else
            {
                sortBy = columnTitle;
                isAscending = true ;
            }
        }
    }
}
