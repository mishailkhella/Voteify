using Vote2.Models;

namespace Vote2.ViewModels
{
    public class UsersViewModel 
    {
        public Int64 UsersId { get; set; }
        public Int64 UserTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
 

        public static implicit operator UsersViewModel(Users users)
        {
            return new UsersViewModel
            {
                UserEmail = users.Email,
                UserName = users.UserName,  
                Email = users.Email,    
                Password = users.Password,  
                Name = users.Name,      
                PhoneNumber = users.PhoneNumber,        
                UsersId = users.Id,
                UserTypeId = users.UserTypeId,

            };       
        }
        public static implicit operator Users(UsersViewModel vm) 
        {
            return new Users
            {
                UserEmail = vm.Email,
                UserName = vm.UserName,
                Email = vm.Email,
                Password = vm.Password,
                Name = vm.Name,
                PhoneNumber = vm.PhoneNumber,
                Id = vm.UsersId,
                UserTypeId = vm.UserTypeId,

            };

        }
    }
}
