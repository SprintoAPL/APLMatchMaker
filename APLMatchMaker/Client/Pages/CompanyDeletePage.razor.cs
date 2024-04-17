using Microsoft.AspNetCore.Components;

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
    }
}