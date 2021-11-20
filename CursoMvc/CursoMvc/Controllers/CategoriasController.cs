using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CursoMvc.Models;
using CursoMvc.Service;

namespace CursoMvc.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly Context _context;
        private readonly CategoriaService categoriaService;
        public CategoriasController(Context context)
        {
            _context = context;
            categoriaService = new CategoriaService();
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Id,Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoriaService.CreateCategoria(categoria);

                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categoria categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Descricao")] Categoria categoria)
        {
            if (id != categoria.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    categoriaService.UpdateCategoria(categoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
                        return NotFound();
                        
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.Id == id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Categoria categoria = await _context.Categorias.FindAsync(id);

            categoriaService.DeleteCategoria(categoria);
            
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
