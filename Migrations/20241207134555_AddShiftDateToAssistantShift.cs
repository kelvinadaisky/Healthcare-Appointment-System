using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_prog_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftDateToAssistantShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ShiftDate",
                table: "AssistantShifts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShiftDate",
                table: "AssistantShifts");
        }
    }
}
