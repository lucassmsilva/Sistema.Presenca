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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento Professor-Turma (one-to-many)
            modelBuilder.Entity<TurmaModel>()
                .HasOne(t => t.Professor)
                .WithMany(p => p.TurmasComoProfessor)
                .HasForeignKey(t => t.IdProfessor);

            // Configuração do relacionamento Alunos-Turma (many-to-many)
            modelBuilder.Entity<TurmaModel>()
                .HasMany(t => t.Alunos)
                .WithMany(p => p.Turmas)
                .UsingEntity(j => j.ToTable("TurmaAlunos")); // Tabela de junção
        }
    }
}
