using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaisonBean.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Addresses");
        }
    }
}
