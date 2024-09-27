using Sistema.Core.Dominio.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Core.Dominio.Models
{
    public class Pessoa : BaseEntity
    {
        public long IdPessoa { get; set; }
        public string Nome { get; set; }

        [StringLength(14)]
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public Pessoa(string nome, string cpf, DateTime? dataNascimento = null)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
