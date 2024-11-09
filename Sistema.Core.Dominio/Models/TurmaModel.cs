namespace Sistema.Core.Dominio.Models
{
    public class TurmaModel : BaseEntity
    {
        public int IdProfessor { get; set; }

        public string NomeTurma { get; set; } = string.Empty;

        public string Sigla { get; set; } = string.Empty;

        // Relacionamento com o professor
        public virtual PessoaModel Professor { get; set; }

        // Relacionamento com os alunos
        public virtual ICollection<PessoaModel> Alunos { get; set; } = new List<PessoaModel>();

        public virtual ICollection<TurmaHorarioModel> Horarios { get; set; } = new List<TurmaHorarioModel>();
    }

}
