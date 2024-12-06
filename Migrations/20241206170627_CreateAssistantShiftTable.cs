using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_prog_Project.Migrations
{
    /// <inheritdoc />
    public partial class CreateAssistantShiftTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssistantShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    AssistantId = table.Column<int>(type: "int", nullable: false),
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistantShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistantShifts_Assistants_AssistantId",
                        column: x => x.AssistantId,
                        principalTable: "Assistants",
                        principalColumn: "AssistantId",
                        onDelete: ReferentialAction.Cascade);
                  
                    table.ForeignKey(
                        name: "FK_AssistantShifts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
              
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssistantShifts_AssistantId",
                table: "AssistantShifts",
                column: "AssistantId");


            migrationBuilder.CreateIndex(
                name: "IX_AssistantShifts_DepartmentId",
                table: "AssistantShifts",
                column: "DepartmentId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssistantShifts");
        }
    }
}
