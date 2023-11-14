using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApiCatalogoContext _context;

        public ProdutosController(ApiCatalogoContext context) // injeção de dependencia
        {
            _context = context;
            // Inicializa o controlador com o contexto do banco de dados.
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                // Método que responde a solicitações HTTP GET para listar produtos.
                var produtos = _context.Produtos.Take(10).AsNoTracking().ToList();
                // Obtém todos os produtos do banco de dados.
                //AsNotTracking método para melhorar o desempenho das consultas,
                //evita armazenar no cache, usarpara consulta somente leituras sem necessidade  de alterar dados
                if (produtos is null)
                {
                   return StatusCode(StatusCodes.Status404NotFound, "Recurso não encontrado.");
                    // Retorna um código de resposta "404 Not Found"
                    // com uma mensagem se não houver produtos.
                }
                return produtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");
            }
        }

        // Endpoint para obter uma produto por ID usando restrição de rota definindo que espera um ID do tipo inteiro maior que 0
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                // Método que responde a solicitações HTTP GET para obter um produto específico por ID.
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Recurso não encontrado.");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");
            }
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try
            {
                // Método que responde a solicitações HTTP POST para criar um novo produto.
                if (produto is null)
                {
                    return BadRequest("Bad Request. Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
                }
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                // Método que responde a solicitações HTTP PUT para atualizar um produto existente.
                if (id != produto.ProdutoId)
                {
                    return BadRequest("Bad Request. Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
                }
                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");       
            }
            
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Método que responde a solicitações HTTP DELETE para excluir um produto por ID.
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound,"Recurso não encontrado.");
                }
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");
            }
            
        }
    }
}
