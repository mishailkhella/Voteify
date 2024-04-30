namespace Vote2.Models
{
    public class VoteInfo : EntityBase
    {
        public Int64 Id { get; set; }
        public Int64 QuestionId { get; set; }
        public Int64 UserId { get; set;}



    }
}
