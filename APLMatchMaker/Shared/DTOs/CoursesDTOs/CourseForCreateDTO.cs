using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.CoursesDTOs
{
    public class CourseForCreateDTO
    {
        [Required(ErrorMessage ="Du måste ange ett namn för kursen.") ]
        public string Name { get; set; } = string.Empty;
        //[MaxLength(100, ErrorMessage ="Beskrivningen kan enbart innehålla 100 tecken.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage ="Du måste ange ett startdatum.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Du måste ange ett slutdatum.")]
        public DateTime EndDate { get; set; }
    }
}
