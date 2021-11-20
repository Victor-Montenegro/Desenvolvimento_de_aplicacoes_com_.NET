using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CursoMvc.Models;
using CursoMvc.Service;
using Microsoft.EntityFrameworkCore.Query;

namespace CursoMvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly Context _context;

        private readonly ProdutoService produtoService;

        public ProdutosController(Context context)
        {
            _context = context;
            produtoService = new ProdutoService();
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            IIncludableQueryable<Produto,Categoria> produtosQuery = _context.Produtos.Include(p => p.Categoria);

            List<Produto> produtos = await produtosQuery.ToListAsync();

            return View(produtos);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            Produto produto = await _context.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(m => m.Id == id);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            SelectList categoriaId = new SelectList(_context.Categorias, "Id", "Descricao");

            ViewData["CategoriaId"] = categoriaId;

            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Descricao,Quantidade,CategoriaId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                produtoService.CreateProduto(produto);
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);

            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Descricao,Quantidade,CategoriaId")] Produto produto)
        {
            if (id != produto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    produtoService.UpdateProduto(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                        return NotFound();
                     
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
           
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Produto produto = await _context.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(m => m.Id == id);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Produto produto = await _context.Produtos.FindAsync(id);

            produtoService.DeleteUpdate(produto);

            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
