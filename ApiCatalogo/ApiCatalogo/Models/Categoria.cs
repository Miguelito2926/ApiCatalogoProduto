using ApiCatalogo.Models;
using System.Collections.ObjectModel; // Importa o namespace para a coleção ObservableCollection.

public class Categoria
{
    public int CategoriaId { get; set; }
    // Propriedade que representa o ID único da categoria.

    public string? Nome { get; set; }
    // Propriedade que representa o nome da categoria.

    public string? ImagemUrl { get; set; }
    // Propriedade que representa a URL da imagem da categoria.

    public ICollection<Produto>? Produtos { get; set; } = new Collection<Produto>();
    // Propriedade de navegação que representa uma coleção de produtos associados a esta categoria.
    // Inicializa a coleção de produtos como uma nova instância de Collection<Produto> por padrão.
}

