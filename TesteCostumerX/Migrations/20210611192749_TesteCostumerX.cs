using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteCostumerX.Migrations
{
    public partial class TesteCostumerX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Contato",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Contato",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Contato");
        }
    }
}
