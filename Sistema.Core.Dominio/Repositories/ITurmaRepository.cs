using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Repositories
{
    public interface ITurmaRepository : IBaseRepository<TurmaModel>
    {
         Task<bool> TurmaExistsAsync(string turma);

    }
}
