using Sistema.Core.Dominio.DTO.Pessoa;
using Sistema.Core.Dominio.DTO.Turma;
using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Dominio.DTO.TurmaHorario
{
    public class TurmaHorarioDTO
    {
        public int Id { get; set; }
        public int IdTurma { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        // Relacionamento
        public virtual TurmaDTO Turma { get; set; }

        public static TurmaHorarioDTO FromEntity(TurmaHorarioModel turmaHorario)
        {
            return new()
            {
                Id = turmaHorario.Id,
                IdTurma = turmaHorario.IdTurma,
                Data = turmaHorario.Data,
                HoraInicio = turmaHorario.HoraInicio,
                HoraFim = turmaHorario.HoraFim,
            };
        }

    }



}
