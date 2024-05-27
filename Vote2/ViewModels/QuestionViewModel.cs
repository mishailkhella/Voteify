using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class QuestionViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 QuestionId { get; set; }
        public string QuestionName { get; set; }
        public Int64 QuestionTypeId { get; set; }
        public Int64 VotedId { get; set; }
        public string QuestionTypeName { get; set; }

        public static implicit operator QuestionViewModel(Question questions)
        {
            return new QuestionViewModel
            {
                Id = questions.Id,
                VotedId = questions.VoteId,
                QuestionTypeId = questions.QuestionTypeId,
                QuestionName = questions.QuestionName,
                QuestionTypeName = questions.QuestionTypeName,
                ModifiedBy = questions.ModifiedBy,
                CreatedBy = questions.CreatedBy,
                ModifiedDate = questions.ModifiedDate,
                CreatedDate = questions.CreatedDate,
                Cancelled = questions.Cancelled,
           

            };       
        }
        public static implicit operator Question(QuestionViewModel vm) 
        {
            return new Question
            {

                Id = vm.Id,
                VoteId = vm.VotedId,
                QuestionTypeName = vm.QuestionTypeName,
                QuestionTypeId = vm.QuestionTypeId,
                QuestionName = vm.QuestionName,
                ModifiedBy = vm.ModifiedBy,
                CreatedBy = vm.CreatedBy,
                ModifiedDate = vm.ModifiedDate,
                CreatedDate = vm.CreatedDate,
                Cancelled = vm.Cancelled,
            };

        }
    }
}
