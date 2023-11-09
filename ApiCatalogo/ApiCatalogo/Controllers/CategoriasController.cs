﻿using ApiCatalogo.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ApiCatalogoContext _context;

        public CategoriasController(ApiCatalogoContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            // Exemplo de método sem filtro, não é uma boa prática
            // return _context.Categorias.Include(p => p.Produtos).ToList();

            // Exemplo de método com filtro, boa prática de consulta de lista
            return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 10).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetAll()
        {
            // Utilizando AsNoTracking para consultas que não precisam rastrear alterações
            return _context.Categorias.AsNoTracking().ToList();
        }

        [HttpGet("{id:int}", Name = "ObterCategorias")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria == null)
            {
                // Retornando NotFound com uma mensagem personalizada
                return NotFound("Recurso não encontrado.");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                // Retornando BadRequest com uma mensagem personalizada
                return BadRequest("Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            // Utilizando CreatedAtRouteResult para retornar um código 201 com a rota de obtenção da categoria criada
            return new CreatedAtRouteResult("ObterCategorias", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                // Retornando BadRequest com uma mensagem personalizada
                return BadRequest("Campos obrigatórios de entrada não enviados ou erros de validação dos campos de entrada.");
            }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            // Retornando Ok com a categoria modificada
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
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
    }
}
