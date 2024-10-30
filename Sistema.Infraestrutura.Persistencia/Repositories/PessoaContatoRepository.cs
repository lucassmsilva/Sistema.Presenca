using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class PessoaContatoRepository : BaseRepository<PessoaContato>, IPessoaContatoRepository
    {
        public PessoaContatoRepository(AppDbContext context): base(context) { }
    }
}
