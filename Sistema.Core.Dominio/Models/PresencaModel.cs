namespace Sistema.Core.Dominio.Models
{
    public class PresencaModel : BaseEntity
    {
        public int IdPessoa { get; set; }
        public int IdTurmaHorario { get; set; }
        public bool Presente { get; set; }

        // Propriedades de navegação
        public PessoaModel? Pessoa { get; set; }
        public TurmaHorarioModel? TurmaHorario { get; set; }
    }
}
