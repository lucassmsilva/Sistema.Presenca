﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sistema.Infraestrutura.Persistencia.Context;

#nullable disable

namespace Sistema.Infraestrutura.Persistencia.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241109013515_TurmaHorario")]
    partial class TurmaHorario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PessoaModelTurmaModel", b =>
                {
                    b.Property<int>("AlunosId")
                        .HasColumnType("int");

                    b.Property<int>("TurmasId")
                        .HasColumnType("int");

                    b.HasKey("AlunosId", "TurmasId");

                    b.HasIndex("TurmasId");

                    b.ToTable("TurmaAlunos", (string)null);
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.PessoaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.PresencaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<int>("IdTurmaHorario")
                        .HasColumnType("int");

                    b.Property<bool>("Presente")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Presencas");
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.TurmaHorarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("HoraFim")
                        .HasColumnType("time(6)");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time(6)");

                    b.Property<int>("IdTurma")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdTurma");

                    b.ToTable("TurmaHorarioModel");
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.TurmaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdProfessor")
                        .HasColumnType("int");

                    b.Property<string>("NomeTurma")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdProfessor");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("PessoaModelTurmaModel", b =>
                {
                    b.HasOne("Sistema.Core.Dominio.Models.PessoaModel", null)
                        .WithMany()
                        .HasForeignKey("AlunosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema.Core.Dominio.Models.TurmaModel", null)
                        .WithMany()
                        .HasForeignKey("TurmasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.TurmaHorarioModel", b =>
                {
                    b.HasOne("Sistema.Core.Dominio.Models.TurmaModel", "Turma")
                        .WithMany("Horarios")
                        .HasForeignKey("IdTurma")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.TurmaModel", b =>
                {
                    b.HasOne("Sistema.Core.Dominio.Models.PessoaModel", "Professor")
                        .WithMany("TurmasComoProfessor")
                        .HasForeignKey("IdProfessor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.PessoaModel", b =>
                {
                    b.Navigation("TurmasComoProfessor");
                });

            modelBuilder.Entity("Sistema.Core.Dominio.Models.TurmaModel", b =>
                {
                    b.Navigation("Horarios");
                });
#pragma warning restore 612, 618
        }
    }
}
