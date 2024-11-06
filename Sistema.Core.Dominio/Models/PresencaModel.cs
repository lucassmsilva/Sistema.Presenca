using Sistema.Core.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Models
{
    public class PresencaModel : BaseEntity
    {
        public int IdPessoa { get; set; }  
        public int IdTurmaHorario { get; set; } 
        public bool Presente { get; set; }  

    }
}
