using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProjeto.Data;
using ApiProjeto.Models;

namespace ApiProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.Categorias
                .ToListAsync();

            return Ok(categorias);
        }


        // POST
        [HttpPost]
        public async Task<IActionResult> CriarCategoria(
     Categoria categoria)
        {
            categoria.Nome =
                char.ToUpper(categoria.Nome[0]) +
                categoria.Nome.Substring(1);

            var categoriaExistente = await _context.Categorias
                .AnyAsync(c => c.Nome == categoria.Nome);

            if (categoriaExistente)
                return BadRequest("Categoria já existe.");

            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return Ok(categoria);
        }
    }
}
   