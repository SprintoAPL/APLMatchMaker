using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{

    public partial class CompanyList : ComponentBase
    {
        [Inject]
        private HttpClient? Http { get; set; }
        public IEnumerable<CompanyForListDTO>? companies { get; set; } = new List<CompanyForListDTO>();
        public string? searchText;
        private string? errorMessage;

        private string sortBy = "Företag";
        private bool isAscending = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            try
            {
                companies = await Http!.GetFromJsonAsync<List<CompanyForListDTO>>("/api/company");
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }


        public async Task Search2()
        {
            try
            {
                companies = await Http!.GetFromJsonAsync<List<CompanyForListDTO>>($"/api/company?searchQuery={searchText}");
                //companies = searchData.Where(x => x.CompanyName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) != -1).ToList();
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        protected async Task AllCompanies()
        {
            await base.OnInitializedAsync();

            try
            {
                companies = await Http!.GetFromJsonAsync<List<CompanyForListDTO>>("/api/company");
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        private string RenderSortIcon(string column)
        {
            if (sortBy == column)
            {
                return isAscending ? "▲" : "▼";
            }
            return string.Empty;
        }

        private void SortByColumn(string column)
        {
            if (sortBy == column)
            {
                isAscending = !isAscending;
            }
            else
            {
                sortBy = column;
                isAscending = true;
            }

            if (companies != null && companies.Any())
            {
                switch (sortBy.ToLower())
                {
                    case "företag":
                        companies = isAscending
                            ? companies.OrderBy(c => c.CompanyName)
                            : companies.OrderByDescending(c => c.CompanyName);
                        break;
                    case "org.nummer":
                        companies = isAscending
                            ? companies.OrderBy(c => c.OrganizationNumber)
                            : companies.OrderByDescending(c => c.OrganizationNumber);
                        break;

                    default:
                        companies = (IEnumerable<CompanyForListDTO>)companies.OrderBy(c => c.CompanyName);
                        break;

                }         

            }

        }
    }
  
}
