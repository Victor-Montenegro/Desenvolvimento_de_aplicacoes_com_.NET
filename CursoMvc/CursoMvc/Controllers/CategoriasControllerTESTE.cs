using CursoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoMvc.Controllers
{
    public class CategoriasControllerTESTE : Controller
    {
        private readonly Context _context;

        public CategoriasControllerTESTE(Context context)
        {
            _context = context;
        }

        // GET : Categorias
        public async Task<IActionResult> Index()
        {

            List<Categoria> categoriaList = await _context.Categorias.ToListAsync();

            return View(categoriaList);
        }

        // GET : Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id.Equals(id.Value));

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // GET categorias/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST : Categorias/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Descricao")] Categoria categoria)
        {
            if (id != categoria.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }


        // GET : Categorias/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            if (categoria == null)
                return NotFound();

            return View(categoria);
        }

        // GET : Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Categoria categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            _context.Remove(categoria);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(x => x.Id == id);
        }
    }
}
