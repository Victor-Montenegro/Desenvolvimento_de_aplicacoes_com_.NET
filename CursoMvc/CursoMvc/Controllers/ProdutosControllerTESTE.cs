using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CursoMvc.Models;

namespace CursoMvc.Controllers
{
    public class ProdutosControllerTESTE : Controller
    {
        private readonly Context _context;

        public ProdutosControllerTESTE()
        {
            _context = new Context();
        }


        //GET: Produtos/Index
        public async Task<IActionResult> Index()
        {
            List<Produto> produtoList = await _context.Produtos.Include(c => c.Categoria).ToListAsync();

            return View(produtoList);
        }

        //GET : Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            Produto produto = await _context.Produtos.Include(c => c.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Id");

            return View();
        }

        // POST : Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao","Quantidade","CategoriaId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Produtos.Add(produto);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriasId"] = new SelectList(_context.Categorias, "Id", "Id", produto.CategoriaId);

            return View(produto);
        }
        
        //GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Produto produto = await _context.Produtos.Include(c => c.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            ViewData["CategoriasId"] = new SelectList(_context.Categorias,"Id","Id");

            return View(produto);
        }


        //POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Descricao","Quantidade","CategoriaId")] Produto produto)
        {
            if (id == produto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Produtos.Update(produto);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["CategoriasId"] = new SelectList(_context.Categorias,"Id","Id");

            return View(produto); 
        }

        //GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id) 
        {
            if(id == null)
                return NotFound();

            Produto produto = await _context.Produtos.Include(c => c.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            return View(produto);
        }


        //POST: Produtos/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,[Bind("Descricao","Quantidade","CategoriaId")] Produto produto)
        {
            if (id != produto.Id)
                return NotFound();

            _context.Produtos.Remove(produto);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(p => p.Id == id);
        }
    }
}
