using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
