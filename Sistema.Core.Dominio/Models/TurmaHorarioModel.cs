using Sistema.Core.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Models
{
    public class TurmaHorarioModel : BaseEntity
    {
        public int IdTurma {get; set;}
        public DateTime DataInicio {get; set;}
        public DateTime DataFim {get; set;}
    }
}
