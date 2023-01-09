using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookinist.DAL.Migrations
{
    /// <inheritdoc />
    public partial class N : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Books_BooksId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "Deals",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_BooksId",
                table: "Deals",
                newName: "IX_Deals_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Books_BookId",
                table: "Deals",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Books_BookId",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Deals",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_Deals_BookId",
                table: "Deals",
                newName: "IX_Deals_BooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Books_BooksId",
                table: "Deals",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
