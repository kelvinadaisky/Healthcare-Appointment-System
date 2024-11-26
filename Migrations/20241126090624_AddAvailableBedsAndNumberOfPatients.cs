using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_prog_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailableBedsAndNumberOfPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "AvailableBeds",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPatients",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableBeds",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "NumberOfPatients",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Departments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
