using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Models
{
    public class TurmaHorarioModel : BaseEntity
    {
        public int IdTurma { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        // Relacionamento
        public virtual TurmaModel Turma { get; set; }
    }
}
