using CursoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoMvc.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly Context _context;

        public CategoriasController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
