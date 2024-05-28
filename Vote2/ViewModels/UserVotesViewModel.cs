using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class UserVotesViewModel
    {
        public Int64 Id { get; set; }
       
        public Int64 VoteId { get; set; }
        public string? VoteName { get; set; }

        public Int64 UserId { get; set; }
        public string? UserName { get; set; }

        public List<VotesViewModel> VotesViewModelList { get; set; }


    }


    public class VotesViewModel
    {
        public Int64 Id { get; set;}
        public string VoteName { get; set; }
        public DateTime EndDate { get; set; }
    }


}
