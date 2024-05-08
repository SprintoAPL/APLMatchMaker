
using APLMatchMaker.Client.Helpers;
using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace APLMatchMaker.Client.Pages
{
    public partial class CompanyEdit
    {
        public CompanyDetailsDTO? companyToUpdate { get; set; } =  new();
        
        private string? ErrorMessage { get; set; }

        [Inject]
        public HttpClient? Http { get; set; }

        [Inject]
        public NavigationManager? NavManager { get; set; }

        [Parameter]
        public int Id { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            { 
                var response = await Http!.GetFromJsonAsync<CompanyDetailsHelper>($"api/company/{Id}");
                
                if (response != null)
                {
                    companyToUpdate = response.company;       
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
                var result = await Http!.PutAsJsonAsync($"api/company/{Id}", companyToUpdate);

                if (result.IsSuccessStatusCode)
                {
                    NavManager!.NavigateTo("/company");
                }
                else
                {
                    ErrorMessage = "Uppdateringen misslyckas ." + result.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }

 }
