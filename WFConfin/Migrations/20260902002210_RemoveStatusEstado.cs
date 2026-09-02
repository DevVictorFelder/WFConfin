using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WFConfin.Migrations
{
    public partial class RemoveStatusEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Estado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Estado",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
