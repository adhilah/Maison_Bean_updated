using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaisonBean.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixWishlistRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "WishlistItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_AppUserId",
                table: "WishlistItems",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_AppUserId",
                table: "CartItems",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppUserId",
                table: "Addresses",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_AppUserId",
                table: "CartItems",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_AspNetUsers_AppUserId",
                table: "WishlistItems",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_AppUserId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_AspNetUsers_AppUserId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_AppUserId",
                table: "WishlistItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_AppUserId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AppUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "WishlistItems");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Addresses");
        }
    }
}
