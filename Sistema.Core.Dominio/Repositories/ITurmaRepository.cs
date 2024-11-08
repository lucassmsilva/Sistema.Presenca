using Sistema.Core.Dominio.DTO.Turma;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;

using System.Linq.Expressions;

namespace Sistema.Core.Dominio.Repositories
{
    public interface ITurmaRepository : IBaseRepository<TurmaModel>
    {
        Task<bool> TurmaExistsAsync(string turma);

        Task<TurmaModel?> ConsultarComAlunos(int id, CancellationToken cancellationToken);

        Task<List<TurmaDTO>> Selecionar(
            Expression<Func<TurmaModel, bool>> filter,
            CancellationToken cancellationToken);
    }
}
