using Sistema.Core.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Models
{
    public class TurmaModel : BaseEntity
    {
        public int IdProfessor { get; set; }

        public string NomeTurma { get; set; } = string.Empty;

        public string Sigla { get; set; } = string.Empty;

    }

}
