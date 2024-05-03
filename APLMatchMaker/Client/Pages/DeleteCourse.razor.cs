using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;
using APLMatchMaker.Client.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class DeleteCourse
    {
        [Inject]
        private HttpClient? Http { get; set; }

        [Inject]
        private NavigationManager? NavManager { get; set; }

        [Parameter]
        public int Id { get; set; }

        private CourseDto CourseToDelete = new();
        private string? ErrorMessage { get; set; }

        private bool IsFeedbackVisible { get; set; } = false;
        private bool IsDeleteSuccess { get; set; }
        private string FeedbackMessage { get; set; } = string.Empty;


        private readonly bool debug = false; // Shows or hides debug data.

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await Http!.GetFromJsonAsync<CourseDto>($"/api/course/{Id}");
                if (response == null)
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
            HttpResponseMessage response;

            try
            {
                response = await Http!.DeleteAsync($"api/course/{Id}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    IsFeedbackVisible = true;
                    IsDeleteSuccess = true;
                    FeedbackMessage = "Kursen har tagits bort.";
                    //NavManager!.NavigateTo($"/ListOfCourses");
                }
                else
                {
                    IsFeedbackVisible = true;
                    IsDeleteSuccess = false;
                    FeedbackMessage = $"Kan inte ta bort kursen: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                IsFeedbackVisible = true;
                IsDeleteSuccess = false;
                FeedbackMessage = $"Fel uppstår vid borttagning av kursen: {ex.Message}";
            }
        }
    }
}
