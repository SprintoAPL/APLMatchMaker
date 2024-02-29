using APLMatchMaker.Shared.TempDTOs;

namespace APLMatchMaker.Client.Pages
{
    public partial class ListOfCourses
    {
        public TempListCoursesDTO? PageListCourses { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            PageListCourses = new TempListCoursesDTO();
        }
    }
}