using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_prog_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "FacultyMembers");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "FacultyMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_DepartmentId",
                table: "FacultyMembers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyMembers_Departments_DepartmentId",
                table: "FacultyMembers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyMembers_Departments_DepartmentId",
                table: "FacultyMembers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_FacultyMembers_DepartmentId",
                table: "FacultyMembers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "FacultyMembers");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "FacultyMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
