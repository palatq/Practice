using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixproblem1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkerId",
                table: "Adjustments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_WorkerId",
                table: "Adjustments",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adjustments_Worker_WorkerId",
                table: "Adjustments",
                column: "WorkerId",
                principalTable: "Worker",
                principalColumn: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adjustments_Worker_WorkerId",
                table: "Adjustments");

            migrationBuilder.DropIndex(
                name: "IX_Adjustments_WorkerId",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Adjustments");
        }
    }
}
