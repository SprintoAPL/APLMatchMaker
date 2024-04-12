using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Pages
{

    public partial class CompanyList : ComponentBase
    {
        [Inject]
        private HttpClient? Http { get; set; }
        public List<CompanyForListDTO>? companies { get; set; }
        private string? errorMessage;
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


        //    [Inject]
        //    private HttpClient? Http { get; set; }
        //    public List<CompanyForListDTO>? PageListCourses { get; set; }
        //    private string? errorMessage;
        //    protected override async Task OnInitializedAsync()
        //    {
        //        await base.OnInitializedAsync();

        //        try
        //        {
        //            PageListCourses = await Http!.GetFromJsonAsync<List<CompanyForListDTO>>("/api/company");
        //        }
        //        catch (Exception exception)
        //        {
        //            errorMessage = exception.Message;
        //        }
        //
  
        
    }
}