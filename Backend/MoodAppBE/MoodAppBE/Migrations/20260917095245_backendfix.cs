using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodAppBE.Migrations
{
    /// <inheritdoc />
    public partial class backendfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMoods_Wellnesses_WellnessId",
                table: "UsersMoods");

            migrationBuilder.DropIndex(
                name: "IX_UsersMoods_WellnessId",
                table: "UsersMoods");

            migrationBuilder.DropColumn(
                name: "WellnessId",
                table: "UsersMoods");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Moods",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "UsersWellnesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsersWellnessId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WellnessId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersWellnesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersWellnesses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersWellnesses_Wellnesses_WellnessId",
                        column: x => x.WellnessId,
                        principalTable: "Wellnesses",
                        principalColumn: "WellnessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersWellnesses_UserId",
                table: "UsersWellnesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWellnesses_WellnessId",
                table: "UsersWellnesses",
                column: "WellnessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersWellnesses");

            migrationBuilder.AddColumn<int>(
                name: "WellnessId",
                table: "UsersMoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Moods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
