using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace APLMatchMaker.Client.Components
{
    public partial class SearchBarStudent
    {
        [Inject]
        public HttpClient? Http {  get; set; }
        private string searchText = "";
        private List<StudentForListDTO>? SearchResult = new List<StudentForListDTO>();

          
        public async Task SearchStudents()
        {
            var response = await Http!.GetAsync($"api/student?SearchQuery={searchText}");
            if (response.IsSuccessStatusCode)
            {
                SearchResult = await response.Content.ReadFromJsonAsync<List<StudentForListDTO>>();
            }
            
        }


    }
}
