using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tangy_DataAccess.Migrations
{
    public partial class ChanenameOrderHeadercolumnStreetAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAdress",
                table: "OrderHeaders",
                newName: "StreetAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "OrderHeaders",
                newName: "StreetAdress");
        }
    }
}
