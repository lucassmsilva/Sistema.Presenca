using Microsoft.EntityFrameworkCore;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;


namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class TurmaRepository : BaseRepository<TurmaModel>, ITurmaRepository
    {
        public TurmaRepository(AppDbContext context): base(context) { }

        public async Task<bool> TurmaExistsAsync(string turma)
        {

            // Verifica se existe alguma pessoa com o CPF fornecido
            return await Context.Turmas
                .AnyAsync(p => p.NomeTurma == turma);
        }
    }
}
