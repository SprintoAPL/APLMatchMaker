using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentListPage
    {
        [Inject]
        private HttpClient? Http { get; set; }

        private IEnumerable<StudentForListDTO>? PageListStudents;
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                PageListStudents = await Http!.GetFromJsonAsync<IEnumerable<StudentForListDTO>>("api/student");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}