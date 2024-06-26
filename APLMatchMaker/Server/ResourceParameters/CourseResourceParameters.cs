﻿using System.ComponentModel.DataAnnotations;

namespace APLMatchMaker.Server.ResourceParameters
{
    public class CourseResourceParameters
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public string? SearchQuery { get; set; }
        public string SortBy { get; set; } = "id";
        public bool IsAscending { get; set; } = true;


    }
}
