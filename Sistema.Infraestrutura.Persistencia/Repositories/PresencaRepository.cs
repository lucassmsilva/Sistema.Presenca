using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class PresencaRepository : BaseRepository<PresencaModel>, IPresencaRepository
    {
        public PresencaRepository(AppDbContext context) : base(context) { }
    }
}
