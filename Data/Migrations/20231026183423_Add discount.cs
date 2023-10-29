using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtlNhom6.Migrations
{
    /// <inheritdoc />
    public partial class Adddiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "dishes",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "dishes");
        }
    }
}
