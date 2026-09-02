using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WFConfin.Migrations
{
    public partial class StatusEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Estado",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Estado");
        }
    }
}
