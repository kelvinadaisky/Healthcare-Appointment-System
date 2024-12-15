using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_prog_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultyMemebrUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "FacultyMembers",
                type: "nvarchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_Email",
                table: "FacultyMembers",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyMembers_AspNetUsers_Email",
                table: "FacultyMembers",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyMembers_AspNetUsers_Email",
                table: "FacultyMembers");

            migrationBuilder.DropIndex(
                name: "IX_FacultyMembers_Email",
                table: "FacultyMembers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "FacultyMembers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)");
        }
    }
}
