using APLMatchMaker.Shared.DTOs.CompanyDTOs;
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

        public CompanyForListDTO? Company { get; set; }
        public string? ErrorMessage { get; set; }



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                Company = await Http!.GetFromJsonAsync<CompanyForListDTO>($"api/company/{Id}");
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
                    NavigationManager!.NavigateTo($"/"); //Index
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