namespace Vote2.Models
{
    public class VoteInfo : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 QuestionId { get; set; }
        public Int64 UserId { get; set;}
        public Int64 FacultyId { get; set; }    
        public Int64 DepartmentId { get; set; }    
        public Int64 SectionId { get; set; }    
        public Int64 LevelId { get; set; }    

        public string VoteName { get; set; }
    }
}
