using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoMvc.Models
{
    public class Categoria : Produto
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
    }
}
