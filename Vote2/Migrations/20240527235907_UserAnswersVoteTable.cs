using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vote2.Migrations
{
    /// <inheritdoc />
    public partial class UserAnswersVoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAnswersVote",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answerdd = table.Column<long>(type: "bigint", nullable: false),
                    VotedUserId = table.Column<long>(type: "bigint", nullable: false),
                    VoteId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    AnswerAext = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswersVote", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnswersVote");
        }
    }
}
