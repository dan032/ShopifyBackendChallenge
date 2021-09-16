using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopifyBackendChallenge.Data.Migrations
{
    public partial class UpdatedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Users_UserModelId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UserModelId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserModelId",
                table: "Images",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Users_UserModelId",
                table: "Images",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
