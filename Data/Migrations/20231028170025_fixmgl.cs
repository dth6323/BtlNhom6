using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtlNhom6.Migrations
{
    /// <inheritdoc />
    public partial class fixmgl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Employees_EmployeeID",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "Menus",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_EmployeeID",
                table: "Menus",
                newName: "IX_Menus_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Employees_EmployeeId",
                table: "Menus",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Employees_EmployeeId",
                table: "Menus");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Menus",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_EmployeeId",
                table: "Menus",
                newName: "IX_Menus_EmployeeID");

            migrationBuilder.AddColumn<int>(
                name: "EmployeesId",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Employees_EmployeeID",
                table: "Menus",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
