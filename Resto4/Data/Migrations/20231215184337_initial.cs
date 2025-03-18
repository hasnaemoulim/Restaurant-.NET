using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resto4.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Plat_PlatId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<int>(
                name: "PlatId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Plat_PlatId",
                table: "OrderDetail",
                column: "PlatId",
                principalTable: "Plat",
                principalColumn: "PlatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Plat_PlatId",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<int>(
                name: "PlatId",
                table: "OrderDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Plat_PlatId",
                table: "OrderDetail",
                column: "PlatId",
                principalTable: "Plat",
                principalColumn: "PlatId");
        }
    }
}
