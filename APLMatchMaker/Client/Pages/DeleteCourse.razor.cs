using APLMatchMaker.Client.Services;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class DeleteCourse
    {
        [Inject]
        private HttpClient? Http { get; set; }
        [Inject]
        ToastService? ToastService { get; set; }
        [Inject]
        private NavigationManager? NavManager { get; set; }

        [Parameter]
        public int Id { get; set; }
        private CourseDto CourseToDelete = new();
        private string? ErrorMessage { get; set; }

        private bool debug = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await Http!.GetFromJsonAsync<CourseDto>($"/api/course/{Id}");
                if ( response == null )
                {
                    ErrorMessage = "Kan inte läsa inhämtade data.";
                    return;
                }
                CourseToDelete = response;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Fel vid inhämtning av data: {ex.Message}";
            }
            await base.OnInitializedAsync();
        }

        private async Task Delete()
        {
            try
            {
                var response = await Http!.DeleteAsync($"api/course/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Course deleted successfully.");
                    ToastService!.ShowToast("Course deleted successfully.", ToastLevel.Success);
                    NavManager!.NavigateTo("/ListOfCourses");
                }
                else
                {
                    Console.WriteLine($"Failed to delete course: {response.StatusCode}");
                    ToastService!.ShowToast($"Failed to delete course: {response.StatusCode}", ToastLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting course: {ex.Message}");
                ToastService!.ShowToast($"Error deleting course: {ex.Message}", ToastLevel.Error);
            }
        }
    }
}
