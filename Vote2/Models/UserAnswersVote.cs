namespace Vote2.Models
{
    public class UserAnswersVote
    {
        public Int64 Id { get; set; }   
        public Int64 AnswerId { get; set; }
        public Int64 VotedUserId { get; set; }
        public Int64 VoteId { get; set; }
        public Int64 QuestionId { get; set; }
        public string? AnswerText { get; set; }
    }
}
