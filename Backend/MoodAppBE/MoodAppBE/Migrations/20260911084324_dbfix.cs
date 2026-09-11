using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodAppBE.Migrations
{
    /// <inheritdoc />
    public partial class dbfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wellnesses",
                columns: table => new
                {
                    WellnessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Food = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SleepQuality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wellnesses", x => x.WellnessId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMoods_WellnessId",
                table: "UsersMoods",
                column: "WellnessId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMoods_Wellnesses_WellnessId",
                table: "UsersMoods",
                column: "WellnessId",
                principalTable: "Wellnesses",
                principalColumn: "WellnessId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMoods_Wellnesses_WellnessId",
                table: "UsersMoods");

            migrationBuilder.DropTable(
                name: "Wellnesses");

            migrationBuilder.DropIndex(
                name: "IX_UsersMoods_WellnessId",
                table: "UsersMoods");
        }
    }
}
