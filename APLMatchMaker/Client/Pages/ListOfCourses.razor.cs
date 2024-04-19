using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using static System.Net.WebRequestMethods;

namespace APLMatchMaker.Client.Pages
{
    public partial class ListOfCourses
    {
        [Inject]
        private HttpClient? Http { get; set; }
        public List<CourseForShortListDTO>? PageListCourses { get; set; }
        private string? errorMessage;
        public string? searchText;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                PageListCourses = await Http!.GetFromJsonAsync<List<CourseForShortListDTO>>("/api/course");
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        public async Task HandleSearch()
        {
            try
            {
                PageListCourses = await Http!.GetFromJsonAsync<List<CourseForShortListDTO>>($"/api/course?searchQuery={searchText}");
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }


    }
}