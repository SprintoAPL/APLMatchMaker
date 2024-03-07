using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class CourseDetails
    {
        [Parameter]
        public CourseDto? Course { get; set; }

    }
}
