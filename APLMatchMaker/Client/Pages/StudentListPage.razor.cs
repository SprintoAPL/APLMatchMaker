using APLMatchMaker.Client.Helpers;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Protocol;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace APLMatchMaker.Client.Pages
{
    public partial class StudentListPage
    {
        [Inject]
        private HttpClient? Http { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public string? navLink { get; set; }
        [Parameter]
        public int CurrentPageIndex { get; set; } = 1;

        private readonly string _apiRoot = "api/student";
        private IEnumerable<StudentForListDTO>? PageListStudents;
        private string? errorMessage;
        private string? pagination;
        private bool debug = false;
        private PaginationMetadata? paginationMetadata;
        private string? searchText;
        private string? searchString = null;
        private string sortBy = "Name";
        private bool isAscending = true;
        private string? sortString = null;
        private string? pageString = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            string order = isAscending ? string.Empty : " desc";
            sortString = string.IsNullOrWhiteSpace(sortBy)
                ? string.Empty
                : $"OrderBy={sortBy}{order}";

            GenerateNavLink();

            await GetDataAsync();
        }

        protected async Task GetDataAsync()
        {
            try
            {
                var response = await Http!.GetAsync(navLink);

                if (response.IsSuccessStatusCode)
                {
                    PageListStudents = await response.Content.ReadFromJsonAsync<IEnumerable<StudentForListDTO>>();
                    var temp = response.Headers.GetValues("X-Pagination").FirstOrDefault().ToJson().Replace("\\\\u0026", "&").Replace("\\", "").Substring(1);
                    var lenth = temp.Length;
                    pagination = temp.Remove(lenth - 1);

                    paginationMetadata = pagination!.FromJson<PaginationMetadata>();
                }
                else
                {
                    errorMessage = response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }

        private void CreateNewStudent()
        {
            // Navigate to the create student page
            NavigationManager!.NavigateTo("/create-student");
        }

        private async Task GoToPrevious()
        {
            navLink = paginationMetadata!.PreviousPageLink;
            await GetDataAsync();
        }

        private async Task GoToNext()
        {
            navLink = paginationMetadata!.NextPageLink;
            await GetDataAsync();
        }

        public async Task SearchStudents()
        {
            pageString = string.Empty;
            searchString = string.IsNullOrWhiteSpace(searchText) ? string.Empty : $"searchQuery={searchText}";
            GenerateNavLink();
            await GetDataAsync();
        }

        private string RenderSortIcon(string columnTitle)
        {
            if (sortBy == columnTitle)
            {
                return isAscending ? "▲" : "▼";
            }
            return "●";
        }

        private async Task SortByColumn(string columnTitle)
        {
            if (sortBy == columnTitle)
            {
                isAscending = !isAscending;
            }
            else
            {
                sortBy = columnTitle;
                isAscending = true;
            }
            string order = isAscending ? string.Empty : " desc";
            pageString = string.Empty;
            sortString = string.IsNullOrWhiteSpace(sortBy) ? string.Empty : $"OrderBy={sortBy}{order}";

            GenerateNavLink();
            await GetDataAsync();
        }

        private async Task GoToPage(int pageIndex)
        {
            pageString = $"PageNumber={pageIndex}";
            GenerateNavLink();

            await GetDataAsync();
        }

        private void GenerateNavLink()
        {
            //Intelligent insert of ? or & into the NavLink.
            var questionMark = string.IsNullOrWhiteSpace(searchString)
                                && string.IsNullOrWhiteSpace(sortString)
                                && string.IsNullOrWhiteSpace(pageString) ? string.Empty : "?";

            var andOne = string.IsNullOrWhiteSpace(searchString) ||
                         (string.IsNullOrWhiteSpace(sortString) && string.IsNullOrWhiteSpace(pageString))
                         ? string.Empty : "&";

            var andTwo = string.IsNullOrWhiteSpace(sortString)
                           || string.IsNullOrWhiteSpace(pageString) ? string.Empty : "&";

            navLink = $"{_apiRoot}{questionMark}{searchString}{andOne}{sortString}{andTwo}{pageString}";

        }

    }
}