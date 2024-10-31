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
    public class TurmaRepository : BaseRepository<TurmaModel>, ITurmaRepository
    {
        public TurmaRepository(AppDbContext context): base(context) { }
    }
}
