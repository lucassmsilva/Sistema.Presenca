using Microsoft.EntityFrameworkCore;

using Sistema.Core.Dominio.DTO.Pessoa;
using Sistema.Core.Dominio.DTO.Presenca;
using Sistema.Core.Dominio.DTO.Turma;
using Sistema.Core.Dominio.DTO.TurmaHorario;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Context;

namespace Sistema.Infraestrutura.Persistencia.Repositories
{
    public class PresencaRepository : BaseRepository<PresencaModel>, IPresencaRepository<PresencaDTO>
    {
        public PresencaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<PresencaDTO>> ObterRegistrosPresenca(int idTurma)
        {
            // Carrega a turma com alunos e horários
            var turma = await Context.Turmas
                .Include(t => t.Professor)    // Inclui o professor
                .Include(t => t.Alunos)
                .Include(t => t.Horarios)
                .Select(t => new TurmaDTO
                {
                    Id = t.Id,
                    NomeTurma = t.NomeTurma,
                    Sigla = t.Sigla,
                    Professor = PessoaDTO.FromEntity(t.Professor),
                    Alunos = t.Alunos.Select(a => PessoaDTO.FromEntity(a)).ToList(),
                    Horarios = t.Horarios.Select(a => TurmaHorarioDTO.FromEntity(a)).ToList()
                })
                .FirstOrDefaultAsync(t => t.Id == idTurma);

            if (turma == null)
                throw new Exception("Turma não encontrada");

            var listaPresencaDTO = new List<PresencaDTO>();

            // Para reduzir o load no retorno
            var turmaDTO = new TurmaDTO
            {
                Id = turma.Id, 
                NomeTurma = turma.NomeTurma,
                Sigla = turma.Sigla,
                Professor = new PessoaDTO  // Criando um novo ProfessorDTO
                {
                    Id = turma.Professor.Id,
                    Nome = turma.Professor.Nome
                }
            };

            // Para cada aluno e horário, registra a presença se não existir
            foreach (var aluno in turma.Alunos)
            {
                foreach (var horario in turma.Horarios)
                {
                    var registroExistente = await Context.Presencas
                        .FirstOrDefaultAsync(p => p.IdPessoa == aluno.Id && p.IdTurmaHorario == horario.Id);

                    if (registroExistente == null)
                    {
                        var novaPresenca = new PresencaModel
                        {
                            IdPessoa = aluno.Id,
                            IdTurmaHorario = horario.Id,
                            Presente = true
                        };

                        Context.Presencas.Add(novaPresenca);
                        await Context.SaveChangesAsync();

                        // Cria o PresencaDTO correspondente
                        PresencaDTO presencaDTO = new()
                        {
                            Id = novaPresenca.Id,
                            IdPessoa = aluno.Id,
                            IdTurmaHorario = horario.Id,
                            Nome = aluno.Nome,
                            Email = aluno.Email,
                            Telefone = aluno.Telefone,
                            Data = horario.Data,
                            Turma = turmaDTO,  // TurmaDTO
                            TurmaHorario = horario,
                            Presente = novaPresenca.Presente
                        };

                        listaPresencaDTO.Add(presencaDTO);

                    }
                    else
                    {
                        // Se o registro de presença já existe, apenas cria o DTO com os dados existentes
                        PresencaDTO presencaDTOExistente = new()
                        {
                            Id = registroExistente.Id,
                            IdPessoa = aluno.Id,
                            IdTurmaHorario = horario.Id,
                            Nome = aluno.Nome,
                            Email = aluno.Email,
                            Telefone = aluno.Telefone,
                            Data = horario.Data,
                            Turma = turmaDTO,  // TurmaDTO
                            TurmaHorario = horario,
                            Presente = registroExistente.Presente
                        };

                        listaPresencaDTO.Add(presencaDTOExistente);
                    }
                }
            }

            return listaPresencaDTO;
        }

        public async Task<bool> RegistrarPresenca(int idPessoa, int idTurmaHorario)
        {
            var presenca = await Context.Presencas
                                    .FirstOrDefaultAsync(p => p.IdPessoa == idPessoa && p.IdTurmaHorario == idTurmaHorario);

            if (presenca != null)
            {
                presenca.Presente = true;
                return true;
            }

            return false;
        }

        public async Task<bool> CancelarPresenca(int idPessoa, int idTurmaHorario)
        {
            var presenca = await Context.Presencas
                .FirstOrDefaultAsync(p => p.IdPessoa == idPessoa && p.IdTurmaHorario == idTurmaHorario);

            if (presenca != null)
            {
                presenca.Presente = false;
                return true;
            }

            return false;
        }

    }
}
