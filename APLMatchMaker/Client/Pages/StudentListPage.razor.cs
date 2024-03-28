using APLMatchMaker.Client.Helpers;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using NuGet.Protocol;
using System.Net.Http.Json;
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

        private IEnumerable<StudentForListDTO>? PageListStudents;
        private string? errorMessage;
        private string? errorMessageTest;
        private string? pagination;
        private bool debug = false;
        private PaginationMetadata? paginationMetadata;

        protected override async Task OnInitializedAsync()
        {
            await GetDataAsync();
            //await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await GetDataAsync();
            //await base.OnParametersSetAsync();
        }

        protected async Task GetDataAsync()
        {
            HttpResponseMessage response;
            if (string.IsNullOrEmpty(navLink))
            {
                navLink = "api/student".Trim();
            }
            else
            {
                navLink = navLink.Trim();
            }

            try
            {
                response = await Http!.GetAsync(navLink);

                if (response.IsSuccessStatusCode)
                {
                    PageListStudents = await response.Content.ReadFromJsonAsync<IEnumerable<StudentForListDTO>>();
                    var temp = response.Headers.GetValues("X-Pagination").FirstOrDefault().ToJson().Replace("\\\\u0026", "&").Replace("\\", "").Substring(1);
                    var lenth = temp.Length;
                    pagination = temp.Remove(lenth - 1);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            try
            {
                paginationMetadata = pagination!.FromJson<PaginationMetadata>();
            }
            catch (Exception ex)
            {
                errorMessageTest = ex.Message;
            }
        }

        // Define Students property
        private void CreateNewStudent()
        {
            // Navigate to the create student page
            NavigationManager!.NavigateTo("/create-student");
        }

        private async Task GoToPrevious()
        {
            navLink = paginationMetadata!.PreviousPageLink;
            await OnParametersSetAsync();
        }

        private async Task GoToNext()
        {
            navLink = paginationMetadata!.NextPageLink;
            await OnParametersSetAsync();
        }
    }
}