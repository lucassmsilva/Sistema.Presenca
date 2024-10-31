using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Aplicacao.UseCases.Pessoa
{
    public class PessoaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }        

        public static PessoaDTO FromEntity(PessoaModel pessoa)
        {
            return new()
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Cpf = pessoa.Cpf,
                DataNascimento = pessoa.DataNascimento,
                Email = pessoa.Email,
                Telefone = pessoa.Telefone,
            };
        }
    }
}