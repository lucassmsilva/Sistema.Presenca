using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class PessoaEnderecoRepository : BaseRepository<PessoaEnderecoModel>, IPessoaEnderecoRepository
    {
        public PessoaEnderecoRepository(AppDbContext context): base(context) { }
    }
}
