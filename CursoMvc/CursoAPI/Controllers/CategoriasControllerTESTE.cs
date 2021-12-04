using CursoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasControllerTESTE : ControllerBase
    {
        private readonly Context _context;

        public CategoriasControllerTESTE(Context context)
        {
            _context = context;
        }

        //GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            List<Categoria> categoria = await _context.Categorias.ToListAsync();

            return categoria;
        }

        //GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            Categoria categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return categoria;
        }

        //POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Add(categoria);
            await _context.SaveChangesAsync();

            var response = new { id = categoria.Id };

            return CreatedAtAction(nameof(GetCategoria), response, categoria);
        }

        //PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> PutCategoria(int id,Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;

           try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!CategoriaExist(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }


        //DELTE: api/Controller/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategorias(int id)
        {
            Categoria categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            _context.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;

        }

        private bool CategoriaExist(int id)
        {
            return _context.Categorias.Any(c => c.Id == id);
        }

    }
}
