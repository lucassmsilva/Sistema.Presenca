using Microsoft.EntityFrameworkCore;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
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
