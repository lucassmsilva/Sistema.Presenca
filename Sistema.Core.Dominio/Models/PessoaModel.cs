using System.ComponentModel.DataAnnotations;

namespace Sistema.Core.Dominio.Models
{
    public class PessoaModel : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;

        [StringLength(14)]
        public string Cpf { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public PessoaModel(string nome, string cpf, DateTime? dataNascimento = null, string email = "", string telefone = "")
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
        }

        // Turmas onde é professor
        public virtual ICollection<TurmaModel> TurmasComoProfessor { get; set; }

        // Turmas onde é aluno
        public virtual ICollection<TurmaModel> Turmas { get; set; }
    }
}
