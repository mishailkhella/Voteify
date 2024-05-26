using System.ComponentModel.DataAnnotations;

namespace Vote2.Models
{
    public class Users : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 UserTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; } //remove
        public string UserEmail { get; set; } //remove
        public string Name { get; set; }
        public Int64 FacultyId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 SectionId { get; set; }
    
    }
}
