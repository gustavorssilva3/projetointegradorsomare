using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetointegrador.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Pergunta_Rec_1 { get; set; }
        public string Pergunta_Rec_2 { get; set; }
        public string Resposta_Rec_1 { get; set; }
        public string Resposta_Rec_2 { get; set; }
    }
}
