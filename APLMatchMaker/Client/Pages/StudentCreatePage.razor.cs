using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentCreatePage : ComponentBase
    {
        private StudentForCreateDTO student = new StudentForCreateDTO(); // Initialize with default values or null
        private string? errorMessage;

        [Inject]
        private HttpClient? Http { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private async Task CreateStudent(StudentForCreateDTO student)
        {
            try
            {
                var response = await Http!.PostAsJsonAsync("api/student", student);
                response.EnsureSuccessStatusCode();
                NavigationManager!.NavigateTo("/ListOfStudents");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
