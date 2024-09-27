using Sistema.Core.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Models
{
    public class PessoaContato : BaseEntity
    {
        public string ?Telefone { get; set; }

        public string? Whatsapp { get; set; }    

        public string? Email { get; set; }   
    }
}
