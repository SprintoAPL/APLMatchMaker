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

        public CompanyForListDTO? companyDetails = new();
        //private CompanyForListDTO companyDetails = new CompanyForListDTO();

        public string? ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {

            try
            {
                //var response = await Http!.GetFromJsonAsync<CompanyForListDTO>($"api/company/{Id}");
                companyDetails = await Http!.GetFromJsonAsync<CompanyForListDTO>($"api/company/{Id}");

                if (companyDetails != null)
                {
                    // companyDetails = response;

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
