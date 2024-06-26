﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Shared.DTOs.CoursesDTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool HasEngagement { get; set; }

        public ICollection<StudentForListDTO>? students { get; set; } = null;
    }
}
