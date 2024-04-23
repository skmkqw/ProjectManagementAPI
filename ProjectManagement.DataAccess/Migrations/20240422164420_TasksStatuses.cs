using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TasksStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectTasks");
        }
    }
}
