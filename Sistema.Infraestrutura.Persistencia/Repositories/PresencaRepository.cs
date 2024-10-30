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
    public class PresencaRepository : BaseRepository<Presenca>, IPresencaRepository
    {
        public PresencaRepository(AppDbContext context): base(context) { }
    }
}
