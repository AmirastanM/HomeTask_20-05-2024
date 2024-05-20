using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorelloBack.Migrations
{
    public partial class createdBlogTabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "SoftDeleted", "Titel" },
                values: new object[] { 1, new DateTime(2024, 5, 15, 18, 19, 55, 368, DateTimeKind.Local).AddTicks(2577), "Reshadin blogu", "blog-feature-img-1.jpg", false, "Titel1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "SoftDeleted", "Titel" },
                values: new object[] { 2, new DateTime(2024, 5, 15, 18, 19, 55, 368, DateTimeKind.Local).AddTicks(2580), "Ilgarin blogu", "blog-feature-img-3.jpg", false, "Titel2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "SoftDeleted", "Titel" },
                values: new object[] { 3, new DateTime(2024, 5, 15, 18, 19, 55, 368, DateTimeKind.Local).AddTicks(2581), "Hacixanin blogu", "blog-feature-img-4.jpg", false, "Titel3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
