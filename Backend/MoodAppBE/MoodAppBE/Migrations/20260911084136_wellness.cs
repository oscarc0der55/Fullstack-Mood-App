using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodAppBE.Migrations
{
    /// <inheritdoc />
    public partial class wellness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "SleepQuality",
                table: "Moods");

            migrationBuilder.AddColumn<int>(
                name: "WellnessId",
                table: "UsersMoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Troubles",
                table: "Moods",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WellnessId",
                table: "UsersMoods");

            migrationBuilder.DropColumn(
                name: "Troubles",
                table: "Moods");

            migrationBuilder.AddColumn<string>(
                name: "Activity",
                table: "Moods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SleepQuality",
                table: "Moods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
