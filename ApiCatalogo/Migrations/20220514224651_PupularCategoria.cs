using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PupularCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT into Categorias(Nome) Values('Bebidas')");
            migrationBuilder.Sql("INSERT into Categorias(Nome) Values('Salgados')");
            migrationBuilder.Sql("INSERT into Categorias(Nome) Values('Doces')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
