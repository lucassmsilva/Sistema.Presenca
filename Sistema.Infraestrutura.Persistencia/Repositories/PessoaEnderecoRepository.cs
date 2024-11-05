using Microsoft.EntityFrameworkCore;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class PessoaEnderecoRepository : BaseRepository<PessoaEnderecoModel>, IPessoaEnderecoRepository
    {
        public PessoaEnderecoRepository(AppDbContext context): base(context) { }
    }
}
