using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class UserAnswerVotesViewModel : EntityBase
    {
        public Int64 Id { get; set; }
       
        public Int64 VoteId { get; set; }
        public string? VoteName { get; set; }
        public DateTime VoteEndDate { get; set; }

        public Int64 UserId { get; set; }
        public string? UserName { get; set; }
        

        public List<VoteQuestionsViewModel> VoteQuestionsList { get; set; }

    }

    public class VoteQuestionsViewModel
    {
        public Int64 Id { get; set;}
        public string QuestionName { get; set; }
        public Int64 QuestionTypeId { get; set; }
        public List<VoteQuestionsAnswersViewModel> VoteQuestionsAnswersList { get; set; }
        public Int64 SelectedAnswerId { get; set; }
        public string? AnswerText { get; set; } //For Posting Text Answer
    }

    public class VoteQuestionsAnswersViewModel
    {
        public Int64 Id { get; set;}
        public string AnswerName { get; set; }
    }


}
