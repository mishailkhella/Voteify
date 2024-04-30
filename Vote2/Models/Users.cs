using System.ComponentModel.DataAnnotations;

namespace Vote2.Models
{
    public class Users
    {
        public Int64 Id { get; set; }
        public Int64 UserTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
    }
}
