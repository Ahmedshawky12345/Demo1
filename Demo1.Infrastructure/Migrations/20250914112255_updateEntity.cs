using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catgories_department_id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "department_id",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_department_id",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catgories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Catgories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catgories_CategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "department_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_department_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catgories_department_id",
                table: "Products",
                column: "department_id",
                principalTable: "Catgories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
