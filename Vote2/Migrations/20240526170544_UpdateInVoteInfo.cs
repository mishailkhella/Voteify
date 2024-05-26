using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vote2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInVoteInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "Votes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FacultyId",
                table: "Votes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LevelId",
                table: "Votes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SectionId",
                table: "Votes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Votes");
        }
    }
}
