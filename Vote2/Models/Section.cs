namespace Vote2.Models
{
    public class Section : EntityBase
    {
        public Int64 Id { get; set; }
        public string  Name { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 DepartmentId { get; set; }
        
     }
}
