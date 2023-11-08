using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApiCatalogoContext _context;

        public ProdutosController(ApiCatalogoContext context)
        {
            _context = context;
            // Inicializa o controleador com o contexto do banco de dados.
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            // Método que responde a solicitações HTTP GET para listar produtos.

            var produtos = _context.Produtos.ToList();
            // Obtém todos os produtos do banco de dados.

            if (produtos is null)
            {
                return NotFound("Recurso não encontrado.");
                // Retorna um código de resposta "404 Not Found" com uma mensagem se não houver produtos.
            }

            return produtos;
            // Retorna a lista de produtos como resultado da solicitação.
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Recurso não encontrado.");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            // produto = _context.Produtos.Find(id);
            if(produto is null)
            {
                return NotFound("Recurso não encontrado.");
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges(); 
            return Ok(produto);
        }
    }
}
