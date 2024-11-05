using Microsoft.EntityFrameworkCore;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;


namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class PessoaRepository : BaseRepository<PessoaModel>, IPessoaRepository
    {
        public PessoaRepository(AppDbContext context) : base(context) { }

        public async Task<bool> CPFExistsAsync(string cpf)
        {
            // Verifica se existe alguma pessoa com o CPF fornecido
            return await Context.Pessoas
                .AnyAsync(p => p.Cpf == cpf);
        }


    }
}
