﻿namespace APLMatchMaker.Server.Models.Entities
{
    public class Enrollment
    {
        public int CourseId { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;


        // Navigation properties.
        public ApplicationUser Student { get; set; } = new();
        public Course Course { get; set; } = new();
    }
}
