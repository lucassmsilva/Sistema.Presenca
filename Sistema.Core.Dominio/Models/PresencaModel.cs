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
        public long IdPessoa { get; set; }  
        public bool Presente { get; set; }  
        public DateTime DataCadastro { get; set; }
        public DateTime HorarioInicio { get; set; }
        public DateTime HorarioFinal { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set;}

    }
}
