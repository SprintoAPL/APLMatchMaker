using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Server.ResourceParameters
{
    public class StudentResourceParameters
    {
        //##-< Filter parameters >-########################################################
        public string? Address { get; set; }
        public string? Status { get; set; }
        public int? KnowledgeLevel { get; set; } // 0 = "Not set", 1 = "Red", 2 = "Yellow", 3 = "Green"
        public bool? CVIntro { get; set; }
        public bool? LinkedinIntro { get; set; }
        public bool? Workshopdag { get; set; }
        public bool? APLSamtal { get; set; }
        public string? Language { get; set; }
        public string? Nationality { get; set; } 
        //#################################################################################


        //##-< Search parameters >-########################################################
        public string? SearchQuery { get; set; }
        //#################################################################################


        //##-< Paging parameters >-########################################################
        const int maxPageSize = 25;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        //#################################################################################


        //##-< ???????????????? >-#########################################################
        //#################################################################################
    }
}
