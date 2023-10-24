using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInOrOutEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Enum",
                table: "ProductWarehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enum",
                table: "ProductWarehouses");
        }
    }
}
