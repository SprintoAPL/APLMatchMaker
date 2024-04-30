using APLMatchMaker.Shared.DTOs.CoursesDTOs;
using Microsoft.AspNetCore.Components;

namespace APLMatchMaker.Client.Components
{
    public partial class CourseList : ComponentBase
    {
        [Parameter]
        public IEnumerable<CourseForShortListDTO> CompListOfCourses { get; set; } = new List<CourseForShortListDTO>();

        private string sortBy = "Name";

        private bool isAscending = true;

        private string RenderSortIcon(string column)
        {
            if (sortBy == column)
            {
                return isAscending ? "▲" : "▼";
            }
            return "string.Empty";
        }

        private void SortByColumn(string column)
        {
            if (sortBy == column)
            {
                isAscending = !isAscending; // Toggle sorting direction
            }
            else
            {
                sortBy = column;
                isAscending = true; // Default to ascending order for new column
            }

            // Perform sorting of CompListOfCourses here based on sortBy and isAscending
            if (CompListOfCourses != null && CompListOfCourses.Any())
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        CompListOfCourses = isAscending
                            ? CompListOfCourses.OrderBy(c => c.Name)
                            : CompListOfCourses.OrderByDescending(c => c.Name);
                        break;
                    case "startdate":
                        CompListOfCourses = isAscending
                            ? CompListOfCourses.OrderBy(c => c.StartDate)
                            : CompListOfCourses.OrderByDescending(c => c.StartDate);
                        break;
                    case "enddate":
                        CompListOfCourses = isAscending
                            ? CompListOfCourses.OrderBy(c => c.EndDate)
                            : CompListOfCourses.OrderByDescending(c => c.EndDate);
                        break;
                    default:
                        // Default sorting by Name in ascending order
                        CompListOfCourses = CompListOfCourses.OrderBy(c => c.Name);
                        break;
                }
            }
        }
    }
}