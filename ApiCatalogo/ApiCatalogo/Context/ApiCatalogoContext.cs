using ApiCatalogo.Models; // Importa o namespace que contém as classes de modelo.

using Microsoft.EntityFrameworkCore; // Importa o namespace do Entity Framework Core para acesso ao banco de dados.

namespace ApiCatalogo.Context
{
    public class ApiCatalogoContext : DbContext
    {
        public ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> options) : base(options)
        {
            // Construtor da classe, que recebe opções de configuração do contexto.
        }

        public DbSet<Categoria>? Categorias { get; set; }
        // Define um DbSet (conjunto) para a entidade "Categoria" que será mapeada para o banco de dados.

        public DbSet<Produto>? Produtos { get; set; }
        // Define um DbSet (conjunto) para a entidade "Produto" que será mapeada para o banco de dados.
    }
}
