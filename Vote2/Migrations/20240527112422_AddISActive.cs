using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vote2.Migrations
{
    /// <inheritdoc />
    public partial class AddISActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Votes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Votes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Votes");

            migrationBuilder.AddColumn<long>(
                name: "QuestionId",
                table: "Votes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
