namespace Vote2.Models
{
    public class VoteInfo : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 FacultyId { get; set; }    
        public Int64 DepartmentId { get; set; }    
        public Int64 SectionId { get; set; }    
        public Int64 LevelId { get; set; }    
        public bool IsActive { get; set; }
        public string VoteName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
