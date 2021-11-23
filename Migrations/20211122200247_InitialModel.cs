using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcPhotoAlbumProject.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Geolocation = table.Column<string>(maxLength: 100, nullable: false),
                    Tags = table.Column<string>(maxLength: 100, nullable: false),
                    CapturedDate = table.Column<DateTime>(nullable: false),
                    CapturedBy = table.Column<string>(maxLength: 255, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");
        }
    }
}
