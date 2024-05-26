using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class UsersViewModel 
    {
        public Int64 UsersId { get; set; }
        public Int64 UserTypeId { get; set; }
        [Required]
        public Int64 FacultyId { get; set; }
        [Required]
        public Int64 DepartmentId { get; set; }
        [Required]
        public Int64 SectionId { get; set; }
        public Int64 LevelId { get; set; }

        [Required(ErrorMessage = "The number is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "The number must be exactly 11 digits.")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

 

        public static implicit operator UsersViewModel(Users users)
        {
            return new UsersViewModel
            {
                UserName = users.UserName,  
                Email = users.Email,    
                Password = users.Password,  
                Name = users.Name,      
                PhoneNumber = users.PhoneNumber,        
                UsersId = users.Id,
                UserTypeId = users.UserTypeId,
                FacultyId = users.FacultyId,
                DepartmentId = users.DepartmentId,
                SectionId = users.SectionId,

            };       
        }
        public static implicit operator Users(UsersViewModel vm) 
        {
            return new Users
            {
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password,
                Name = vm.Name,
                PhoneNumber = vm.PhoneNumber,
                Id = vm.UsersId,
                UserTypeId = vm.UserTypeId,
                FacultyId=vm.FacultyId,
                DepartmentId = vm.DepartmentId,
                SectionId = vm.SectionId,


            };

        }
    }
}
