namespace Vote2.Models
{
    public class UserAnswersVote
    {
        public Int64 Id { get; set; }   
        public Int64 Answerdd { get; set; }
        public Int64 VotedUserId { get; set; }
        public Int64 VoteId { get; set; }
        public Int64 QuestionId { get; set; }
        public string? AnswerAext { get; set; }
    }
}
