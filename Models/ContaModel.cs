using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetointegrador.Models
{
    public class ContaModel
    {
        public string NomeCliente { get; set; }
        public DateTime DataVencimento { get; set; }
        public string NomeConta { get; set; }
        public decimal ValorConta { get; set; }
        public string TipoConta { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public int IdCadastro { get; set; }
        public int Id { get; internal set; }
    }
}
