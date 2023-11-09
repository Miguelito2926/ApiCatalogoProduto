using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models;
[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    // Propriedade que representa o ID único do produto.

    [Required] // Especifica que o valor do campo é obrigatório
    [MaxLength(80)] // Defini o tamanho minimo ou maximo permitido para o tipo
    public string? Nome { get; set; }
    // Propriedade que representa o nome do produto.

    [Required]
    [MaxLength(300)]
    public string? Descricao { get; set; }
    // Propriedade que representa a descrição do produto.

    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }
    // Propriedade que representa o preço do produto.

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    // Propriedade que representa a URL da imagem do produto.

    public float Estoque { get; set; }
    // Propriedade que representa a quantidade em estoque do produto.
   
    public DateTime DataCadastro { get; set; }
    // Propriedade que representa a data de cadastro do produto.

 
    public int CategoriaId { get; set; }
    // Propriedade que representa o ID da categoria do produto.

    [JsonIgnore] // Ignora a propriedade no Json
    public Categoria? Categoria { get; set; }
    // Propriedade de navegação que representa a categoria associada ao produto.
}
