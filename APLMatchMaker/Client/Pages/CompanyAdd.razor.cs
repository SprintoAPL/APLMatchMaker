using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class CompanyAdd
    {
        [Inject]
        HttpClient? Http { get; set; }
        [Inject]
        NavigationManager? NavManager { get; set; }

        public CompanyForCreateDTO CompanyToAdd { get; set; } = new();
        public string? ErrorMessage;

        private async Task AddCompany(CompanyForCreateDTO CompanyToAdd)
        {
            try
            {
                var response = await Http!.PostAsJsonAsync("api/company/", CompanyToAdd);
                if (response.IsSuccessStatusCode)
                {
                    NavManager?.NavigateTo($"/company");
                }
                else
                {
                    ErrorMessage = $"Kan inte lägga till företaget {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }
    }
}