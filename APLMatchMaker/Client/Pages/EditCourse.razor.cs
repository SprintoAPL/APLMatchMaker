using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class EditCourse
    {
        [Inject]
        public HttpClient? Http {  get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Parameter]
        public int Id {  get; set; }
        private CourseForEditDto CourseToEdit { get; set; } = new();
        private string? ErrorMessage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await Http!.GetFromJsonAsync<CourseForEditDto>($"api/course/{Id}");
                if ( response != null ) 
                {
                    CourseToEdit = response;
                }
                else
                {
                    ErrorMessage = "Kan inte hämta data.";
                }
            }   
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task ValidateSubmit()
        {
            try
            {
                var result = await Http!.PutAsJsonAsync($"api/course/{Id}", CourseToEdit);

                if (result.IsSuccessStatusCode)
                {
                    NavManager!.NavigateTo("/ListOfCourses");
                }
                else
                {
                    ErrorMessage = "Uppdateringen misslyckas ."+ result.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        

    }
}
