using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrderId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ProductEntityId",
                table: "ProductOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductEntityId",
                table: "ProductOrders",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Products_ProductEntityId",
                table: "ProductOrders",
                column: "ProductEntityId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Products_ProductEntityId",
                table: "ProductOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_ProductEntityId",
                table: "ProductOrders");

            migrationBuilder.DropColumn(
                name: "ProductEntityId",
                table: "ProductOrders");

            migrationBuilder.AddColumn<int>(
                name: "ProductOrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
