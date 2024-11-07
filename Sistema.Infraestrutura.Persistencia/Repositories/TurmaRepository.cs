using Microsoft.EntityFrameworkCore;
using Sistema.Core.Aplicacao.UseCases.Pessoa;
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
                    Professor = new PessoaDTO
                    {
                        Id = t.Professor.Id,
                        Nome = t.Professor.Nome
                    },
                    Alunos = t.Alunos.Select(a => new PessoaDTO
                    {
                        Id = a.Id,
                        Nome = a.Nome
                    }).ToList()
                })
                .ToListAsync(cancellationToken);
        }
    }
}
