using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.CompanyDTOs
{
    public class CompanyProjectsShortListDTO
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }


        public ICollection<CompanyInternshipsShortListDTO>? Internships { get; set; }
    }
}
