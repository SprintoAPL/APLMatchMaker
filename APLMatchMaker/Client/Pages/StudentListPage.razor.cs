using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Pages
{
    public partial class ListOfStudents 
    {
        [Inject]
        private HttpClient Http { get; set; }
        
        public IEnumerable<StudentForListDTO> PageListStudents;

        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
           await base.OnInitializedAsync();

            try
            {
                PageListStudents = await Http.GetFromJsonAsync<IEnumerable<StudentForListDTO>>("api/student");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
