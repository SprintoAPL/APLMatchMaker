using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using APLMatchMaker.Client.Helpers;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class CompanyDeletePage
    {
        [Inject]
        private HttpClient? Http { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public int Id { get; set; }

        public CompanyDetailsDTO? Company { get; set; }
        public string? ErrorMessage { get; set; }



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                var respose = await Http!.GetFromJsonAsync<CompanyDetailsHelper>($"api/company/{Id}");
                Company = respose!.company;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading company with ID:{Id},: " + ex.Message;
            }
        }

        private async Task Delete()
        {
            try
            {
                var response = await Http!.DeleteAsync($"api/company/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager!.NavigateTo($"/company");
                }
                else
                {
                    ErrorMessage = "Failed to delete company. Error: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error deleting company: " + ex.Message;
            }
        }
    }
}