using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BtlNhom6.Migrations
{
    /// <inheritdoc />
    public partial class Last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuDetails_Employees_EmployeeID",
                table: "MenuDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_AspNetUsers_ApplicationUserId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_ApplicationUserId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_MenuDetails_EmployeeID",
                table: "MenuDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenuName",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "MenuDetails");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeesId",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Menus",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_EmployeeID",
                table: "Menus",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_UserId",
                table: "Menus",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_AspNetUsers_UserId",
                table: "Menus",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Employees_EmployeeID",
                table: "Menus",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_AspNetUsers_UserId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Employees_EmployeeID",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_EmployeeID",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_UserId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "EmployeesId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Menus");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Menus",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuName",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "MenuDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ApplicationUserId",
                table: "Menus",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuDetails_EmployeeID",
                table: "MenuDetails",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuDetails_Employees_EmployeeID",
                table: "MenuDetails",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_AspNetUsers_ApplicationUserId",
                table: "Menus",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
