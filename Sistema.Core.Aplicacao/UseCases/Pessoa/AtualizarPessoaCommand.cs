using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Aplicacao.UseCases.Pessoa
{
    public class AtualizarPessoaCommand
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public string Telefone { get; set; }
        public string Email { get; set; }

        public PessoaModel MapToPessoa(PessoaModel pessoa)
        {
            pessoa.Nome = Nome;
            pessoa.Cpf = CPF;
            pessoa.DataNascimento = DataNascimento;
            pessoa.Email = Email;
            pessoa.Telefone = Telefone; 

            return pessoa;
        }
    }

}
