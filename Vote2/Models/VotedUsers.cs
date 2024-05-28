namespace Vote2.Models
{
    public class VotedUsers
    {
        public Int64 Id { get; set; }   
        public Int64 UserId { get; set; }
        public Int64 VotedId { get; set; }
        public DateTime VoteDate { get; set; }
    }
}
