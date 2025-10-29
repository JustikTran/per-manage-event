using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageEventBackend.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbV103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "Events",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "Events");
        }
    }
}
