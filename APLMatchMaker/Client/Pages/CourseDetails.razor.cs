using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class CourseDetails
    {
        [Inject]
        public HttpClient? Http { get; set; }
        [Parameter]
        public int Id { get; set; }
        public CourseDto Course { get; set; } = new CourseDto();
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await Http!.GetFromJsonAsync<CourseDto>($"api/course/{Id}");
                if (response != null)
                {
                    Course = response;
                }
                else
                {
                    ErrorMessage = "Kan inte läsa in inhämtade data.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}
