using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vote2.Migrations
{
    /// <inheritdoc />
    public partial class AnswerTextInUserAnswersVote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerAext",
                table: "UserAnswersVote",
                newName: "AnswerText");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerText",
                table: "UserAnswersVote",
                newName: "AnswerAext");
        }
    }
}
