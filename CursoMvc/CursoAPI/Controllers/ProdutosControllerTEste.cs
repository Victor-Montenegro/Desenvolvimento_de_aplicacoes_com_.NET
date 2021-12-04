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
    public class ProdutosControllerTEste : ControllerBase
    {
        private readonly Context _context;

        public ProdutosControllerTEste(Context context)
        {
            _context = context;
        }

        //GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            List<Produto> produtos = await _context.Produtos.Include(p => p.Categoria).ToListAsync();

            return produtos;
        }

        //GET/ api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            Produto produto = await _context.Produtos.Include("Categoria").FirstOrDefaultAsync(p => p.Id == id);


            if (produto == null)
                return BadRequest();

            return produto;
        }

        //POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            object response = new { id = produto.Id };

            return CreatedAtAction(nameof(GetProduto), response, produto);
        }

        //PUT: api/Produtos/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Produto>> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
                return BadRequest();

            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExist(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            Produto produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound();

            _context.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        private bool ProdutoExist(int id)
        {
            return _context.Produtos.Any(p => p.Id == id);
        }
    }
}
