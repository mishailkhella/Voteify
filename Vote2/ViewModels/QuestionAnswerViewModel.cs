using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class QuestionAnswerViewModel : EntityBase
    {
        public Int64 Id { get; set; }
        public string? AnswerName { get; set; }
        public Int64 QuestionId { get; set; }
        public Int64 VoteId { get; set; }

        public static implicit operator QuestionAnswerViewModel(QuestionAnswer _questionAnswer)
        {
            return new QuestionAnswerViewModel
            {
                Id = _questionAnswer.Id,
                VoteId = _questionAnswer.VoteId,
                AnswerName = _questionAnswer.AnswerName,
                QuestionId = _questionAnswer.QuestionId,
                ModifiedBy = _questionAnswer.ModifiedBy,
                CreatedBy = _questionAnswer.CreatedBy,
                ModifiedDate = _questionAnswer.ModifiedDate,
                CreatedDate = _questionAnswer.CreatedDate,
                Cancelled = _questionAnswer.Cancelled,
           

            };       
        }
        public static implicit operator QuestionAnswer(QuestionAnswerViewModel vm) 
        {
            return new QuestionAnswer
            {

                Id = vm.Id,
                VoteId = vm.VoteId,
                AnswerName = vm.AnswerName,
                QuestionId = vm.QuestionId,
                ModifiedBy = vm.ModifiedBy,
                CreatedBy = vm.CreatedBy,
                ModifiedDate = vm.ModifiedDate,
                CreatedDate = vm.CreatedDate,
                Cancelled = vm.Cancelled,
            };

        }
    }
}
