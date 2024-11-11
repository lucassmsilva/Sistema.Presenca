using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Interfaces
{
    public interface IPresenca
    {
        public int IdPessoa { get; set; }
        public int IdTurmaHorario { get; set; }
        public bool Presente { get; set; }  
    }
}
