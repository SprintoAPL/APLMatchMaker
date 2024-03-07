using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLMatchMaker.Shared.DTOs.StudentDTOs
{
    public class StudentForShortListDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string StudentSocSecNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public int PhoneNumber { get; set; }

    }
}
