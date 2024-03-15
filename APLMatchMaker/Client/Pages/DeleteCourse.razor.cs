using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class DeleteCourse
    {
        [Inject]
        private HttpClient? Http { get; set; }
        private NavigationManager? NavManager { get; set; }

        [Parameter]
        public int Id { get; set; }
        private CourseDto CourseToDelete = new CourseDto();
        private ListOfCoursesDTO Courses = new ListOfCoursesDTO();
        private string? ErrorMessage { get; set; }

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
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    NavManager!.NavigateTo($"/ListOfCourses");
                }
                else
                {
                    ErrorMessage = $"Kan inte ta bort kursen: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Fel uppstår vid borttagning av kursen: {ex.Message}";
            }
        }
    }
}
