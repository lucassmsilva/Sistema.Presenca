using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Dominio.Repositories
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
         Task<bool> CPFExistsAsync(string cpf);

    }
}
