using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.Pessoa
{
    public class AtualizarPessoaCommand
    {
        public long Id { get; set; }    
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
    
}
