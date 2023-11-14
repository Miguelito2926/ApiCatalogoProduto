// Importando namespaces necessários
using ApiCatalogo.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Definindo o namespace e declarando a classe do controlador CategoriasController
namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        // Declaração do contexto do banco de dados
        private readonly ApiCatalogoContext _context;

        // Construtor que recebe o contexto do banco de dados como parâmetro
        public CategoriasController(ApiCatalogoContext context)
        {
            _context = context;
        }

        // Endpoint para obter todas as categorias com seus produtos
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                // Exemplo de método sem filtro, não é uma boa prática
                // return _context.Categorias.Include(p => p.Produtos).ToList();
                // Exemplo de método com filtro, boa prática de consulta de lista
                return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 10).ToList();
            }
            catch (Exception)
            {
                // Retornando um erro interno do servidor em caso de exceção
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");
            }
        }

        // Endpoint para obter todas as categorias
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAll()
        {
            // Utilizando AsNoTracking para consultas que não precisam rastrear alterações
            return _context.Categorias.AsNoTracking().ToList();
        }

        // Endpoint para obter uma categoria por ID usando restrição de rota definindo que espera um ID do tipo inteiro maior que 0
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
                if (categoria == null)
                {
                    // Retornando NotFound com uma mensagem personalizada
                    return NotFound("Recurso não encontrado.");
                }

                // Retornando Ok com a categoria encontrada
                return Ok(categoria);
            }
            catch (Exception)
            {
                // Retornando um erro interno do servidor em caso de exceção
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");
            }
        }

        // Endpoint para adicionar uma nova categoria
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    // Retornando BadRequest com uma mensagem personalizada
                    return BadRequest("Bad Request. Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
                }
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                // Utilizando CreatedAtRouteResult para retornar um código 201 com a rota de obtenção da categoria criada
                return new CreatedAtRouteResult("ObterCategoria",
                        new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                // Retornando um erro interno do servidor em caso de exceção
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Internal Server Error. Solicitação não enviada. Precisa ser executada novamente.");
            }
        }

        // Endpoint para atualizar uma categoria existente
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                // Retornando BadRequest com uma mensagem personalizada
                return BadRequest("Bad Request. Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
            }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            // Retornando Ok com a categoria modificada
            return Ok(categoria);
        }

        // Endpoint para excluir uma categoria
        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    // Retornando NotFound com uma mensagem personalizada
                    return NotFound("Recurso não encontrado.");
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();

                // Retornando Ok com a categoria removida
                return Ok(categoria);
            }
            catch (Exception)
            {
                // Retornando BadRequest com uma mensagem personalizada
                return BadRequest("Bad Request. Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
            }
        }
    }
}
