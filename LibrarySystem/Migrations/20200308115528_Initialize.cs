using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibrarySystem.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Author = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Isbn = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    FullName = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Rental__BookId__5CD6CB2B",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Rental__Customer__5BE2A6F2",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "Isbn", "Title" },
                values: new object[,]
                {
                    { 1, "John Travolta", "12312123132", "Sun is shining" },
                    { 2, "Angelina Jolie", "342545667765", "Master that game" },
                    { 3, "Larry Clint", "756324vddsffd", "Stop sending SMS" },
                    { 4, "Haskel Peanut", "12312123132", "I like peanutbutter" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426655440000"), "Andrzej Duda" },
                    { new Guid("123e5432-e89b-12d3-a456-426655440000"), "Barack Obama" },
                    { new Guid("213e4567-e89b-12d3-a456-426655440000"), "Tristan Burba" }
                });

            migrationBuilder.InsertData(
                table: "Rental",
                columns: new[] { "Id", "BookId", "CustomerId", "EndDate", "StartDate" },
                values: new object[] { 1, 2, new Guid("123e4567-e89b-12d3-a456-426655440000"), null, new DateTime(2012, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Rental",
                columns: new[] { "Id", "BookId", "CustomerId", "EndDate", "StartDate" },
                values: new object[] { 2, 4, new Guid("213e4567-e89b-12d3-a456-426655440000"), new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_BookId",
                table: "Rental",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_CustomerId",
                table: "Rental",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
