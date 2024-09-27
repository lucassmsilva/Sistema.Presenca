using Sistema.Core.Dominio.Interfaces;
using Sistema.Infraestrutura.Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class UnitOfWork : IUnityOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
