using Microsoft.EntityFrameworkCore;

using Sistema.Core.Dominio.Models;

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

            modelBuilder.Entity<TurmaHorarioModel>()
            .HasOne(th => th.Turma)
            .WithMany(t => t.Horarios)
            .HasForeignKey(th => th.IdTurma);

            // Configurações adicionais para TurmaHorario
            modelBuilder.Entity<TurmaHorarioModel>()
                .Property(th => th.Data)
                .IsRequired();

            modelBuilder.Entity<TurmaHorarioModel>()
                .Property(th => th.HoraInicio)
                .IsRequired();

            modelBuilder.Entity<TurmaHorarioModel>()
                .Property(th => th.HoraFim)
                .IsRequired();

            modelBuilder.Entity<PresencaModel>()
            .HasOne(p => p.Pessoa)
            .WithMany()
            .HasForeignKey(p => p.IdPessoa);

            modelBuilder.Entity<PresencaModel>()
                .HasOne(p => p.TurmaHorario)
                .WithMany()
                .HasForeignKey(p => p.IdTurmaHorario);
        }
    }
}
