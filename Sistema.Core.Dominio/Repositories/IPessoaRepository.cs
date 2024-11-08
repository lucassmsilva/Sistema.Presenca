using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;

namespace Sistema.Core.Dominio.Repositories
{
    public interface IPessoaRepository : IBaseRepository<PessoaModel>
    {
        Task<bool> CPFExistsAsync(string cpf);

    }
}
