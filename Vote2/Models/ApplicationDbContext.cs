namespace Vote2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options)
        {

        }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerVote> VotesAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Questiontype> Questionstype { get; set; }
        public DbSet<Usertype> Usertype { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<VoteInfo> Votes { get; set; }

    }

}
