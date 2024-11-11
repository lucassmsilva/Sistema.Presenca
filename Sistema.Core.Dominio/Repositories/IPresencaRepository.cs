using Sistema.Core.Dominio.DTO.Presenca;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Dominio.Repositories
{
    public interface IPresencaRepository<T>  : IBaseRepository<PresencaModel> where T : IPresenca
    {
        Task<bool> RegistrarPresenca(int idPessoa, int idTurmaHorario);
        Task<bool> CancelarPresenca(int idPessoa, int idTurmaHorario);
        Task<IEnumerable<T>> ObterRegistrosPresenca(int idTurma);
    }
}
