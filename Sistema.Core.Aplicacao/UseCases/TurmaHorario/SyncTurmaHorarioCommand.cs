using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.TurmaHorario
{
    public class SyncTurmaHorarioCommand
    {
        public int IdTurma { get; set; }
        public string HoraInicio { get; set; } = string.Empty;// formato "19:00"
        public string HoraFim { get; set; } = string.Empty;  // formato "22:00"
        public List<string> Datas { get; set; } = new(); // formato "11/11/2024"
    }
}
