using static System.Collections.Specialized.BitVector32;

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
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public DbSet<AnswerVote> AnswerVote { get; set; }
        public DbSet<VotedUsers> VotedUsers { get; set; }
        public DbSet<Userslevels> UserLevels { get; set; }
    }
}
