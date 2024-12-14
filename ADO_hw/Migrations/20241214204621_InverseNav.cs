using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADO_hw.Migrations
{
    /// <inheritdoc />
    public partial class InverseNav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Managers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Managers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SecDepId",
                table: "Managers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_Login",
                table: "Managers",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_ManagerId",
                table: "Managers",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_SecDepId",
                table: "Managers",
                column: "SecDepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_SecDepId",
                table: "Managers",
                column: "SecDepId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Managers_ManagerId",
                table: "Managers",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_SecDepId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Managers_ManagerId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_Login",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_ManagerId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_SecDepId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "SecDepId",
                table: "Managers");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
