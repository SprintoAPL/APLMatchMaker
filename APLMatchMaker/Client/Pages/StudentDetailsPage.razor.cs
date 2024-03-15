using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentDetailsPage
    {
        [Inject]
        public HttpClient? Http { get; set; }

        [Parameter]
        public string? Id { get; set; }

        public StudentForDetailsDTO studentDetails = new StudentForDetailsDTO();

        private string? ErrorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await Http!.GetFromJsonAsync<StudentForDetailsDTO>($"api/student/{Id}");
                if(response != null)
                {
                    studentDetails = response;
                }
                else
                {
                    ErrorMessage = "Could not read Student details from API!";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        
    }
}
