using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "InStock", "Price", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, "Essential Programming Language ", true, 250, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " Essential Programming Language " },
                    { 2, " Telling Arts ", true, 245, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), " Telling Arts " },
                    { 3, " Marvel ", true, 150, new DateTime(2019, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), " Marvel " },
                    { 4, " The Beauty of Cook ", true, 450, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), " The Beauty of Cook" },
                    { 5, " Learning how to Cook  ", true, 450, new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), " Learning how to Cook " }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
