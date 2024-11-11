using Sistema.Core.Dominio.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.Presenca
{
    public class PresencaCommand : IPresenca
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public int IdTurmaHorario { get; set; }
        public bool Presente { get; set; }
    }
}
