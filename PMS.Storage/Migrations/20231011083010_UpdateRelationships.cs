using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductWarehouse",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductWarehouse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouse_ProductsId",
                table: "ProductWarehouse",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse");

            migrationBuilder.DropIndex(
                name: "IX_ProductWarehouse_ProductsId",
                table: "ProductWarehouse");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductWarehouse");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductWarehouse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse",
                columns: new[] { "ProductsId", "WarehouseId" });
        }
    }
}
