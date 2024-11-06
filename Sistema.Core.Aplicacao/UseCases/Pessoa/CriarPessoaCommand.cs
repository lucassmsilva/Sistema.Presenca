
using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Aplicacao.UseCases.Pessoa
{
    public class CriarPessoaCommand
    {
        public string Nome { get; set; }= string.Empty;
        public string CPF { get; set; }= string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;


        public PessoaModel ToPessoa() => new PessoaModel(
            Nome,
            CPF,
            DataNascimento,
            Email,
            Telefone
        );
    }
}
