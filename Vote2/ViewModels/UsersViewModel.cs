using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class UsersViewModel : EntityBase
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

        [Required(ErrorMessage = "The phone number is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "The number must be exactly 11 digits.")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

 

        public static implicit operator UsersViewModel(Users users)
        {
            return new UsersViewModel
            {
                Email = users.Email,    
                Password = users.Password,  
                Name = users.Name,      
                PhoneNumber = users.PhoneNumber,        
                UsersId = users.Id,
                UserTypeId = users.UserTypeId,
                FacultyId = users.FacultyId,
                DepartmentId = users.DepartmentId,
                SectionId = users.SectionId,

                ModifiedBy = users.ModifiedBy,
                CreatedBy = users.CreatedBy,
                ModifiedDate = users.ModifiedDate,
                CreatedDate = users.CreatedDate,
                Cancelled = users.Cancelled,
            };       
        }
        public static implicit operator Users(UsersViewModel vm) 
        {
            return new Users
            {
                Email = vm.Email,
                Password = vm.Password,
                Name = vm.Name,
                PhoneNumber = vm.PhoneNumber,
                Id = vm.UsersId,
                UserTypeId = vm.UserTypeId,
                FacultyId=vm.FacultyId,
                DepartmentId = vm.DepartmentId,
                SectionId = vm.SectionId,

                ModifiedBy = vm.ModifiedBy,
                CreatedBy = vm.CreatedBy,
                ModifiedDate = vm.ModifiedDate,
                CreatedDate = vm.CreatedDate,
                Cancelled = vm.Cancelled,
            };

        }
    }
}
