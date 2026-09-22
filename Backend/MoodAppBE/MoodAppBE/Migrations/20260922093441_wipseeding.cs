using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodAppBE.Migrations
{
    /// <inheritdoc />
    public partial class wipseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMoods_AspNetUsers_UserId",
                table: "UsersMoods");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersWellnesses_AspNetUsers_UserId",
                table: "UsersWellnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersWellnesses",
                table: "UsersWellnesses");

            migrationBuilder.DropIndex(
                name: "IX_UsersWellnesses_UserId",
                table: "UsersWellnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersMoods",
                table: "UsersMoods");

            migrationBuilder.DropIndex(
                name: "IX_UsersMoods_UserId",
                table: "UsersMoods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersWellnesses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersMoods");

            migrationBuilder.AlterColumn<int>(
                name: "UsersWellnessId",
                table: "UsersWellnesses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UsersWellnesses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsersMoodId",
                table: "UsersMoods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UsersMoods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersWellnesses",
                table: "UsersWellnesses",
                column: "UsersWellnessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersMoods",
                table: "UsersMoods",
                column: "UsersMoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWellnesses_Id",
                table: "UsersWellnesses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMoods_Id",
                table: "UsersMoods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMoods_AspNetUsers_Id",
                table: "UsersMoods",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersWellnesses_AspNetUsers_Id",
                table: "UsersWellnesses",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMoods_AspNetUsers_Id",
                table: "UsersMoods");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersWellnesses_AspNetUsers_Id",
                table: "UsersWellnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersWellnesses",
                table: "UsersWellnesses");

            migrationBuilder.DropIndex(
                name: "IX_UsersWellnesses_Id",
                table: "UsersWellnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersMoods",
                table: "UsersMoods");

            migrationBuilder.DropIndex(
                name: "IX_UsersMoods_Id",
                table: "UsersMoods");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UsersWellnesses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsersWellnessId",
                table: "UsersWellnesses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersWellnesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UsersMoods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UsersMoodId",
                table: "UsersMoods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersMoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersWellnesses",
                table: "UsersWellnesses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersMoods",
                table: "UsersMoods",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsersWellnesses_UserId",
                table: "UsersWellnesses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMoods_UserId",
                table: "UsersMoods",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMoods_AspNetUsers_UserId",
                table: "UsersMoods",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersWellnesses_AspNetUsers_UserId",
                table: "UsersWellnesses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
