using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CF_Sample_Api.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyRelationshipWithBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BookPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AuthorId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorId", "BookPrice", "BookTitle", "Isbn" },
                values: new object[,]
                {
                    { 1L, 3L, 100.0m, "Sample Book 1", null },
                    { 2L, 3L, 500.41m, "Sample Book 2", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                table: "Book",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
