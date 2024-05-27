using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class VoteInfoViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        [Required]
        public Int64 FacultyId { get; set; }
        [Required]
        public Int64 DepartmentId { get; set; }
        [Required]
        public Int64 SectionId { get; set; }
        public Int64 LevelId { get; set; }
        public string VoteName { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public static implicit operator VoteInfoViewModel(VoteInfo Votes)
        {
            return new VoteInfoViewModel
            {
                Id = Votes.Id,
                IsActive = Votes.IsActive,
                FacultyId = Votes.FacultyId,
                LevelId = Votes.LevelId,
                DepartmentId = Votes.DepartmentId,
                SectionId = Votes.SectionId,
                VoteName = Votes.VoteName,
                StartDate = Votes.StartDate,
                EndDate = Votes.EndDate,
                ModifiedBy = Votes.ModifiedBy,
                CreatedBy = Votes.CreatedBy,
                ModifiedDate = Votes.ModifiedDate,
                CreatedDate = Votes.CreatedDate,
                Cancelled = Votes.Cancelled,
           

            };       
        }
        public static implicit operator VoteInfo(VoteInfoViewModel vm) 
        {
            return new VoteInfo
            {
                
                Id = vm.Id,
                IsActive = vm.IsActive,
                FacultyId =vm.FacultyId,
                LevelId =vm.LevelId,
                DepartmentId = vm.DepartmentId,
                SectionId = vm.SectionId,
                VoteName = vm.VoteName, 
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                ModifiedBy = vm.ModifiedBy,
                CreatedBy = vm.CreatedBy,
                ModifiedDate = vm.ModifiedDate,
                CreatedDate = vm.CreatedDate,
                Cancelled = vm.Cancelled,
            };

        }
    }
}
