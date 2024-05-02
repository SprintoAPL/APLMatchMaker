using Microsoft.AspNetCore.Components;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Client.Components
{
    public partial class StudentList : ComponentBase
    {
        [Parameter]
        public  IEnumerable<StudentForListDTO> Students { get; set; } = new List<StudentForListDTO>();

        private string sortBy = "Name";
        private bool isAscending = true;

        private string RenderSortIcon(string columnTitle)
        {
            if (sortBy == columnTitle)
            {
                return isAscending ? "▲" : "▼";
            }
            return "■";
        }
        
        private void SortByColumn(string columnTitle)
        {
            if (sortBy == columnTitle)
            {
                isAscending = !isAscending;
            }
            else
            {
                sortBy = columnTitle;
                isAscending = true ;
            }

            if (Students !=null && Students.Any())
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        Students = isAscending
                            ? Students.OrderBy(c => c.Name)
                            : Students.OrderByDescending(c => c.Name);
                        break;

                    case "e-post":
                        Students = isAscending
                            ? Students.OrderBy(c => c.Email)
                            : Students.OrderByDescending(c => c.Email);
                        break;

                    case "personnummer":
                        Students = isAscending
                            ? Students.OrderBy(c => c.StudentSocSecNo)
                            : Students.OrderByDescending(c => c.StudentSocSecNo);
                        break;

                    case "adress":
                        Students = isAscending
                            ? Students.OrderBy(c => c.Address)
                            : Students.OrderByDescending(c => c.Address);
                        break;

                    case "kunskapsnivå":
                        Students = isAscending
                            ? Students.OrderBy(c => c.KnowledgeLevel)
                            : Students.OrderByDescending(c => c.KnowledgeLevel);
                        break;

                    case "språk":
                        Students = isAscending
                            ? Students.OrderBy(c => c.Language)
                            : Students.OrderByDescending(c => c.Language);
                        break;

                    default:
                        Students = Students.OrderBy(c => c.Name);
                        break;
                }
            }
        }
    }
}
