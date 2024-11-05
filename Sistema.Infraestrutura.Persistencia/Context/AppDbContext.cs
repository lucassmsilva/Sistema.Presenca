using Microsoft.EntityFrameworkCore;
using Sistema.Core.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Infraestrutura.Persistencia.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PessoaModel> Pessoas { get; set; }
        public DbSet<TurmaModel> Turmas { get; set; }
        public DbSet<PresencaModel> Presencas { get; set; }
    }
}
