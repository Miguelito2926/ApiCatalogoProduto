namespace ApiCatalogo.Models;

public class Produto
{
    public int ProdutoId { get; set; }
    // Propriedade que representa o ID único do produto.

    public string? Nome { get; set; }
    // Propriedade que representa o nome do produto.

    public string? Descricao { get; set; }
    // Propriedade que representa a descrição do produto.

    public decimal Preco { get; set; }
    // Propriedade que representa o preço do produto.

    public string? ImagemUrl { get; set; }
    // Propriedade que representa a URL da imagem do produto.

    public float Estoque { get; set; }
    // Propriedade que representa a quantidade em estoque do produto.

    public DateTime DataCadastro { get; set; }
    // Propriedade que representa a data de cadastro do produto.

    public int CategoriaId { get; set; }
    // Propriedade que representa o ID da categoria do produto.

    public Categoria? Categoria { get; set; }
    // Propriedade de navegação que representa a categoria associada ao produto.
}
