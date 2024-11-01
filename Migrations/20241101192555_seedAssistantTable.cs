using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_prog_Project.Migrations
{
    /// <inheritdoc />
    public partial class seedAssistantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assistants",
                columns: new[] { "AssistantId", "Email", "FirstName", "LastName", "Phone", "Specialization" },
                values: new object[,]
                {
                    { 1, "kelvin@gmail.com", "Kelvin", "Irutingabo", "+257 77492508", "Kids" },
                    { 2, "kelvin2002@gmail.com", "Joujou", "Ndayizeye", "+257 77730599", "baby" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assistants",
                keyColumn: "AssistantId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assistants",
                keyColumn: "AssistantId",
                keyValue: 2);
        }
    }
}
