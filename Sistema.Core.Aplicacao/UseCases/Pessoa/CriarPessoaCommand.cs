
using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Aplicacao.UseCases.Pessoa
{
    public class CriarPessoaCommand
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }


        public PessoaModel ToPessoa() => new PessoaModel(
            Nome,
            CPF,
            DataNascimento,
            Email,
            Telefone
        );
    }
}
