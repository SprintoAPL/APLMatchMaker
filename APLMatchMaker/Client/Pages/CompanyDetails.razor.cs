using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace APLMatchMaker.Client.Pages
{
    public partial class CompanyDetails
    {

        [Inject]
        public HttpClient? Http { get; set; }

        [Parameter]
        public int Id { get; set; }
        public CompanyDetailsDTO companyDetails = new();
        public string? ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await Http!.GetFromJsonAsync<CompanyDetailsDTO>($"api/Company/{Id}");
                if (response != null)
                {
                    companyDetails = response;
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