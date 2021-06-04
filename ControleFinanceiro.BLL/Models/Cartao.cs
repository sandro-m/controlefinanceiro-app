using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.BLL.Models
{
    public class Cartao
    {
        public int CartaoId { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
        public string Numero { get; set; }
        public double Limite { get; set; }
        public string UsuarioId { get; set; } // Por que UsuarioId é um string? Pois vamos utilizar o Identity e lá os Ids dos usuarios sao string
        public Usuario Usuario { get; set; }
        public virtual ICollection<Despesa> Despesas { get; set; }

    }
}
