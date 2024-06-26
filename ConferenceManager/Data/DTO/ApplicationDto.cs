﻿namespace ConferenceManager.Data.DTO
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid Author { get; set; }
        public ActivityDto Activity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Outline { get; set; }
        public DateTime? SubmittedAt { get; internal set; }
        public DateTime? CreatedAt { get; internal set; }
    }
}
