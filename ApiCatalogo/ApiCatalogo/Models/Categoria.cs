using ApiCatalogo.Models;
using System.Collections.ObjectModel; // Importa o namespace para a coleção ObservableCollection.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Categorias")]
public class Categoria
{
    [Key] // Defini a chave Primaria
    public int CategoriaId { get; set; }
    // Propriedade que representa o ID único da categoria.

    
    [Required] // Especifica que o valor do campo é obrigatório
    [MaxLength(80)]//Defini o tamanho mínimo ou máximo permitido para o tipo
    public string? Nome { get; set; }
    // Propriedade que representa o nome da categoria.

    [Required] 
    [MaxLength(300)]
    public string? ImagemUrl { get; set; }
    // Propriedade que representa a URL da imagem da categoria.

    public ICollection<Produto>? Produtos { get; set; } = new Collection<Produto>();
    // Propriedade de navegação que representa uma coleção de produtos associados a esta categoria.
    // Inicializa a coleção de produtos como uma nova instância de Collection<Produto> por padrão.
}

