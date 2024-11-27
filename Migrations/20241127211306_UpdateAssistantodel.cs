using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_prog_Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssistantodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assistants",
                keyColumn: "AssistantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assistants",
                keyColumn: "AssistantId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Assistants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Assistants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Assistants",
                columns: new[] { "AssistantId", "Email", "FirstName", "LastName", "Phone", "Specialization" },
                values: new object[,]
                {
                    { 1, "kelvin@gmail.com", "Kelvin", "Irutingabo", "+257 77492508", "Kids" },
                    { 2, "kelvin2002@gmail.com", "Joujou", "Ndayizeye", "+257 77730599", "baby" }
                });
        }
    }
}
