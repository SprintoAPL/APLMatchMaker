using APLMatchMaker.Shared.DTOs.CoursesDTOs;

namespace APLMatchMaker.Client.Pages
{
    public partial class ListOfCourses
    {
        public ListOfCoursesDTO? PageListCourses { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            PageListCourses = new ListOfCoursesDTO();
        }
    }
}