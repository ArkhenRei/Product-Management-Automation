using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Products_ProductsId",
                table: "ProductWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouse_Warehouse_WarehouseId",
                table: "ProductWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse");

            migrationBuilder.RenameTable(
                name: "ProductWarehouse",
                newName: "ProductWarehouses");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouse_WarehouseId",
                table: "ProductWarehouses",
                newName: "IX_ProductWarehouses_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouse_ProductsId",
                table: "ProductWarehouses",
                newName: "IX_ProductWarehouses_ProductsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouses",
                table: "ProductWarehouses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouses_Products_ProductsId",
                table: "ProductWarehouses",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouses_Warehouse_WarehouseId",
                table: "ProductWarehouses",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouses_Products_ProductsId",
                table: "ProductWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductWarehouses_Warehouse_WarehouseId",
                table: "ProductWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductWarehouses",
                table: "ProductWarehouses");

            migrationBuilder.RenameTable(
                name: "ProductWarehouses",
                newName: "ProductWarehouse");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouses_WarehouseId",
                table: "ProductWarehouse",
                newName: "IX_ProductWarehouse_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductWarehouses_ProductsId",
                table: "ProductWarehouse",
                newName: "IX_ProductWarehouse_ProductsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductWarehouse",
                table: "ProductWarehouse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Products_ProductsId",
                table: "ProductWarehouse",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductWarehouse_Warehouse_WarehouseId",
                table: "ProductWarehouse",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
