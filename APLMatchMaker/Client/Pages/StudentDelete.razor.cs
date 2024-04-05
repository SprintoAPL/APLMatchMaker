using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentDelete
    {
        [Parameter]
        public string? Id { get; set; }

        private StudentForDetailsDTO? studentDelete;

        private string? ErrorMessage;

        [Inject]
        private HttpClient? Http { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                studentDelete = await Http!.GetFromJsonAsync<StudentForDetailsDTO>($"api/student/{Id}");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error loading student details: " + ex.Message;
            }
        }

        private async Task Delete()
        {
            try
            {
                var response = await Http!.DeleteAsync($"api/student/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager!.NavigateTo($"/ListOfStudents");
                }
                else
                {
                    ErrorMessage = "Failed to delete student. Error: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error deleting student: " + ex.Message;
            }
        }
    }
}
