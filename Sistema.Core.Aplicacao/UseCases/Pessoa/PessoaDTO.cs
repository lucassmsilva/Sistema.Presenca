using DomainPessoa = Sistema.Core.Dominio.Models.Pessoa;

namespace Sistema.Core.Aplicacao.UseCases.Pessoa
{
    public class PessoaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }

        public static PessoaDTO FromEntity(DomainPessoa pessoa)
        {
            return new PessoaDTO
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Cpf = pessoa.Cpf,
                DataNascimento = pessoa.DataNascimento,
                Idade = DateTime.Now.Year - pessoa.DataNascimento.Year
            };
        }
    }
}