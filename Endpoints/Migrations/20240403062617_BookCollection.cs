using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Endpoints.Migrations
{
    /// <inheritdoc />
    public partial class BookCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCollectionId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BooksCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksCollections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCollectionId",
                table: "Books",
                column: "BookCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BooksCollections_BookCollectionId",
                table: "Books",
                column: "BookCollectionId",
                principalTable: "BooksCollections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BooksCollections_BookCollectionId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BooksCollections");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookCollectionId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookCollectionId",
                table: "Books");
        }
    }
}
