using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PupularProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT into Produtos (Nome,Descricao,Preco,ImagemURL,Estoque,DataCadastro,CategoriaId)" + "Values('Coca Cola','Refrigerante de 350 Ml',5.45,'cocacola.png',50,now(),1)");
            migrationBuilder.Sql("INSERT into Produtos (Nome,Descricao,Preco,ImagemURL,Estoque,DataCadastro,CategoriaId)" + "Values('Coxinha De Frango', 'Coxinha é vida foda-se',7.20,'coxinha.png',20,now(),2)");
            migrationBuilder.Sql("INSERT into Produtos (Nome,Descricao,Preco,ImagemURL,Estoque,DataCadastro,CategoriaId)" + "Values('Brigadeiro','Chocolate com leite condenado e uma fruta do karai',1.50,'brigadeiro.png',10,now(),3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos");
        }
    }
}
