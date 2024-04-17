using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class CompanyDetailsComponent
    {
        [Parameter]
        public CompanyForListDTO Company { get; set; } = new();
    }
}