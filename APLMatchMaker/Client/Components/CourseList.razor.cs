using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;
using APLMatchMaker.Client.Pages;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Components
{
    public partial class CourseList : ComponentBase
    {
        [Parameter]
        public IEnumerable<CourseForShortListDTO>? CompListOfCourses { get; set; } = new List<CourseForShortListDTO>();
        [Inject]
        private HttpClient? Http { get; set; }

        public string? errorMessage;

        public string? searchText;

        public async Task HandleSearch()
        {
            try
            {
                CompListOfCourses = await Http!.GetFromJsonAsync<List<CourseForShortListDTO>>($"/api/course?searchQuery={searchText}");
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }
    }
}