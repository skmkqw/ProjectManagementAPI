using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPulse.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TaskPriorities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ProjectTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ProjectTask",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ProjectTask");
        }
    }
}
