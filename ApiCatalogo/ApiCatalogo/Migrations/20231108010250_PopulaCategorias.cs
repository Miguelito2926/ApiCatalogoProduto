using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        // Classe de migração para popular a tabela de Categorias com dados iniciais.

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Método chamado durante a aplicação da migração (atualização do banco de dados).

            migrationBuilder.Sql("Insert into Categorias(Nome, ImagemUrl) Values ('Bebidas', 'bebidas.jpg')");
            // Insere uma nova categoria "Bebidas" com uma URL de imagem associada.

            migrationBuilder.Sql("Insert into Categorias(Nome, ImagemUrl) Values ('Lanches', 'lanches.jpg')");
            // Insere uma nova categoria "Lanches" com uma URL de imagem associada.

            migrationBuilder.Sql("Insert into Categorias(Nome, ImagemUrl) Values ('Sobremesas', 'sobremesas.jpg')");
            // Insere uma nova categoria "Sobremesas" com uma URL de imagem associada.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Método chamado durante a reversão da migração (desfazendo a atualização do banco de dados).

            migrationBuilder.Sql("Delete from Categorias");
            // Remove todos os registros da tabela "Categorias".
        }
    }
}
