using System.ComponentModel.DataAnnotations;
using Vote2.Models;

namespace Vote2.ViewModels
{
    public class VotesReportViewModel
    {
        public Int64 Id { get; set; }
        public Int64 VoteId { get; set; }
        public string? VoteName { get; set; }
        public DateTime VoteEndDate { get; set; }
        public int NoOfVoters { get; set; }
        public List<QuestionsReportViewModel> QuestionsReportList { get; set; }
    }

    public class QuestionsReportViewModel
    {
        public Int64 Id { get; set;}
        public string QuestionName { get; set; }
        public Int64 QuestionTypeId { get; set; }
        public List<QuestionsAnswersReportViewModel> QuestionsAnswersReportList { get; set; }
    }

    public class QuestionsAnswersReportViewModel
    {
        public Int64 Id { get; set;}
        public string AnswerName { get; set; }
        public string? UserName { get; set; }
        public string? AnswerText { get; set; }
        public DateTime VoteDate { get; set; }
        public double Percentage { get; set; }
    }


}
