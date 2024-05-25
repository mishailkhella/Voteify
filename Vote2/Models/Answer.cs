namespace Vote2.Models
{
    public class Answer : EntityBase 
    {
        public Int64 Id { get; set; }
        public Int64 QuestionId { get; set; }
        public string? AnswerQestion { get; set; }
        

    }
}
