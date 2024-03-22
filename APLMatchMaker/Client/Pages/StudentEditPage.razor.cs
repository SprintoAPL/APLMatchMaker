using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentEditPage
    {
        private StudentForUpdateDTO editStudent = new StudentForUpdateDTO();

        private string? ErrorMessage;

        [Inject]
        private HttpClient? Http { get; set; }

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public string? Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                editStudent = await Http!.GetFromJsonAsync<StudentForUpdateDTO>($"api/student/{Id}");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task HandleValidSubmit()
        {
            try
            {
                var response = await Http!.PatchAsJsonAsync($"api/student/{Id}", editStudent);
                if (!response.IsSuccessStatusCode)
                {
                    NavigationManager!.NavigateTo("/studentdetails");
                }
                else
                {
                    ErrorMessage = "Kunde inte redigera eleven!";
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            
        }

        private void Cancel()
        {
            NavigationManager?.NavigateTo("/ListOfStudents");
        }


    }
}
