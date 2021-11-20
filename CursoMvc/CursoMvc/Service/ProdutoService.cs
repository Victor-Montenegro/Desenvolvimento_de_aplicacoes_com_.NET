using CursoMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoMvc.Service
{
    public class ProdutoService
    {

        public async void CreateProduto(Produto produto, Context context = null)
        {
            bool isCommitHere = false;

            if(context == null)
            {
                context = new Context();
                isCommitHere = true;
            }

            context.Add(produto);

            if (isCommitHere)
                await context.SaveChangesAsync();
        }

        public async void UpdateProduto(Produto produto, Context context = null)
        {
            bool isCommitHere = false;

            if(context == null)
            {
                context = new Context();
                isCommitHere = true;
            }

            context.Update(produto);

            if (isCommitHere)
                await context.SaveChangesAsync();
        }

        public async void DeleteUpdate(Produto produto, Context context = null)
        {
            bool isCommitHere = false;

            if(context == null)
            {
                context = new Context();
                isCommitHere = true;
            }

            context.Produtos.Remove(produto);

            if (isCommitHere)
                await context.SaveChangesAsync();
        }
    }
}
