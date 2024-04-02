using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http.Json;
using System.Text.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class AddCourse
    {
        [Inject]
        HttpClient? Http { get; set; }
        [Inject]
        NavigationManager? NavManager { get; set; }

        public CourseDto Course { get; set; } = new();
        public string? ErrorMessage;

        private async Task AddNewCourse(CourseDto course)
        {
            try
            {
                var response = await Http!.PostAsJsonAsync("api/course/", course);
                if (response.IsSuccessStatusCode)
                {
                    NavManager?.NavigateTo($"/ListOfCourses");
                }
                else 
                {
                    ErrorMessage = $"Kan inte lägga till kursen {response.StatusCode}";  
                }
            }
            catch(Exception ex) 
            {
                ErrorMessage = ex.Message;
            }

        } 


    }
}
