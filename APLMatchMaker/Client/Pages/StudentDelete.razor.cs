using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentDelete
    {
        [Inject]
        public HttpClient? Http { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public string? Id { get; set; }

        private StudentForListDTO studentDelete = new StudentForListDTO();

        private string? ErrorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                studentDelete = await Http!.GetFromJsonAsync<StudentForListDTO>($"api/student/{Id}");
                if (studentDelete == null)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            await base.OnInitializedAsync();
        }
        private async Task Delete()
        {
            try
            {
                var response = await Http!.DeleteAsync($"/api/Student/" + Id);
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager!.NavigateTo($"/ListOfStudents");
                }
                else
                {
                    ErrorMessage = "Could not delete a student in API!" + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting a student: {ex.Message}";
            }
        }
    }
}
