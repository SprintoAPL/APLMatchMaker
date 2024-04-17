using APLMatchMaker.Shared.DTOs.CompanyDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class CompanyDetailsComponent
    {
        [Parameter]
        public CompanyDetailsDTO Company { get; set; } = new();
    }
}