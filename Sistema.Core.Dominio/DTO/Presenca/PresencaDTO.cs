using Sistema.Core.Dominio.DTO.Turma;
using Sistema.Core.Dominio.DTO.TurmaHorario;
using Sistema.Core.Dominio.Interfaces;

namespace Sistema.Core.Dominio.DTO.Presenca
{
    public class PresencaDTO : IPresenca
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public int IdTurmaHorario { get; set; }
        public string Nome { get; set; } = string.Empty;// Nome do aluno
        public string Email { get; set; } = string.Empty;// Email do aluno
        public string Telefone { get; set; } = string.Empty;// Telefone do aluno
        public DateTime Data { get; set; }  // Data da presença
        public TurmaHorarioDTO TurmaHorario { get; set; } // Horário
        public TurmaDTO Turma { get; set; } // Turma
        public bool Presente { get; set; }  // Status da presença

        // Construtor padrão necessário para restrição new()
        public PresencaDTO() { }

        // Construtor opcional para facilitar a criação do DTO com esses campos
        public PresencaDTO(int id, string nome, string email, string telefone, DateTime data, TurmaHorarioDTO turmaHorario, TurmaDTO turma, bool presente)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Data = data;
            TurmaHorario = turmaHorario;
            Turma = turma;
            Presente = presente;
        }
    }
}
