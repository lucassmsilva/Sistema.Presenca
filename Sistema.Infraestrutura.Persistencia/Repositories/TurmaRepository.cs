using Microsoft.EntityFrameworkCore;

using Sistema.Core.Dominio.DTO.Pessoa;
using Sistema.Core.Dominio.DTO.Turma;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;

using System.Linq.Expressions;


namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class TurmaRepository : BaseRepository<TurmaModel>, ITurmaRepository
    {
        public TurmaRepository(AppDbContext context) : base(context) { }

        public async Task<bool> TurmaExistsAsync(string turma)
        {

            // Verifica se existe alguma pessoa com o CPF fornecido
            return await Context.Turmas
                .AnyAsync(p => p.Sigla == turma);
        }
        public async Task<List<TurmaDTO>> Selecionar(
    Expression<Func<TurmaModel, bool>> filter,
    CancellationToken cancellationToken)
        {
            return await Context.Set<TurmaModel>()
                .Include(t => t.Professor)    // Inclui o professor
                .Include(t => t.Alunos)       // Inclui os alunos
                .Where(filter)
                .Select(t => new TurmaDTO
                {
                    Id = t.Id,
                    NomeTurma = t.NomeTurma,
                    Sigla = t.Sigla,
                    Professor = PessoaDTO.FromEntity(t.Professor),
                    Alunos = t.Alunos.Select(a => PessoaDTO.FromEntity(a)).ToList()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<TurmaModel> ConsultarComAlunos(int id, CancellationToken cancellationToken)
        {
            return await Context.Set<TurmaModel>().Include(t => t.Alunos).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
