﻿namespace Vote2.Models
{
    public class Question : EntityBase
    {
        public Int64 Id { get; set; }
        public string QuestionName { get; set; }
        public DateTime? StartDate  { get; set; }
        public DateTime? EndDate { get; set; }
        public Int64 UserType { get; set; } 
        public Int64 QuestionTypeId  { get; set;}
    }
}
