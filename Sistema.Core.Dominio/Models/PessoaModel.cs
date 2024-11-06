using Sistema.Core.Dominio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Core.Dominio.Models
{
    public class PessoaModel : BaseEntity
    {
        public string Nome { get; set; }= string.Empty;

        [StringLength(14)]
        public string Cpf { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }

        public string? Telefone { get; set; }

        public string? Email { get; set; } 
        public PessoaModel(string nome, string cpf, DateTime? dataNascimento = null, string telefone = "", string email = "")
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
        }
    }
}
