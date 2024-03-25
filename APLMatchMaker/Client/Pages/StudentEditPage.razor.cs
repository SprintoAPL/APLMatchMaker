using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentEditPage
    {
        private StudentForUpdateDTO? editStudent;
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
                // Check if editStudent is not null
                if (editStudent != null)
                {
                    // Construct the JSON patch document
                    var patchDocument = new JsonPatchDocument<StudentForUpdateDTO>();
                    patchDocument.Replace(s => s.FirstName, editStudent.FirstName);
                    patchDocument.Replace(s => s.LastName, editStudent.LastName);

                    // Send the PATCH request with the JSON patch document
                    var serializedPatchDoc = JsonConvert.SerializeObject(patchDocument);

                    var request = new HttpRequestMessage(HttpMethod.Patch, $"api/student/{Id}");
                    request.Content = new StringContent(serializedPatchDoc);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await Http!.SendAsync(request);


                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // If successful, navigate to the list of students
                        NavigationManager?.NavigateTo("/ListOfStudents");
                    }
                    else
                    {
                        // If not successful, parse the error response and display error message
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        ErrorMessage = string.IsNullOrEmpty(errorResponse) ? "An error occurred while updating the student." : errorResponse;
                    }
                }
                else
                {
                    ErrorMessage = "Student data is missing.";
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs during the API call, display the exception message
                ErrorMessage = ex.Message;
            }
        }

        private void Cancel()
        {
            NavigationManager?.NavigateTo("/ListOfStudents");
        }
    }
}
