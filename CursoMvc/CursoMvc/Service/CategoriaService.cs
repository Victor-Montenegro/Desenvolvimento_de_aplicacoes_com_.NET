using CursoMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoMvc.Service
{
    public class CategoriaService
    {
        public async void CreateCategoria(Categoria categoria,Context context = null)
        {
            bool isCommitHere = false;

            if (context == null)
            {
                context = new Context();
                isCommitHere = true;
            }

            context.Add(categoria);

            if (isCommitHere)
                await context.SaveChangesAsync();
        }

        public async void UpdateCategoria(Categoria categoria, Context context = null)
        {
            bool isCommitHere = false;

            if(context == null)
            {
                context = new Context();
                isCommitHere = true;
            }

            context.Update(categoria);

            if (isCommitHere)
                await context.SaveChangesAsync();
        }

        public async void DeleteCategoria(Categoria categoria, Context context = null)
        {
            bool isCommitHere = false;

            if(context == null)
            {
                context = new Context();
                isCommitHere = true;
            }

            context.Categorias.Remove(categoria);

            if (isCommitHere)
                await context.SaveChangesAsync();
        }
    }
}
