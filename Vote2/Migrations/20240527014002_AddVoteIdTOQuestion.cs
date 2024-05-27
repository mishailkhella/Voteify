using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vote2.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteIdTOQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VoteId",
                table: "Questions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteId",
                table: "Questions");
        }
    }
}
