namespace Vote2.Models
{
    public class QuestionAnswer : EntityBase
    {
        public Int64 Id { get; set; }
        public string AnswerName { get; set; }
        public Int64 QuestionId { get; set; }
        public Int64 VoteId { get; set; }

    }
}
