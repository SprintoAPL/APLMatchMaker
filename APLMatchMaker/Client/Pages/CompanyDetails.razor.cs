using APLMatchMaker.Client.Helpers;
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
                var response = await Http!.GetFromJsonAsync<CompanyDetailsHelper>($"api/company/{Id}");
                //companyDetails = await Http!.GetFromJsonAsync<CompanyForListDTO>($"api/company/{Id}");

                if (response != null)
                {
                     companyDetails = response.company;

                }
                else
                {
                    ErrorMessage = "Kan inte l�sa in inh�mtade data.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
