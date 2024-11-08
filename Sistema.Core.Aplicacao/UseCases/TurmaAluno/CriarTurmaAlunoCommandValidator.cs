using FluentValidation;
using Sistema.Core.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.TurmaAluno
{
    public class CriarTurmaAlunoCommandValidator : AbstractValidator<CriarTurmaAlunoCommand>
    {
        public readonly ITurmaRepository _turmaRepository;
        public readonly IPessoaRepository _pessoaRepository;

        public int IdProfessor { get; set; }
        public CriarTurmaAlunoCommandValidator(ITurmaRepository turmaRepository, IPessoaRepository pessoaRepository)
        {
            _turmaRepository = turmaRepository;
            _pessoaRepository = pessoaRepository;




            RuleFor(x => x.IdTurma)
                .NotEmpty()
                .MustAsync(TurmaExists)
                .WithMessage("Turma não cadastrado")
                .DependentRules(() =>
                {

                    RuleFor(x => x.Pessoas)
                   .MustAsync(ProfessorNaoAluno)
                   .WithMessage("Professor não pode ser aluno");

                    RuleFor(x => x.Pessoas)
                     .MustAsync(PessoaExists)
                     .WithMessage("Aluno não cadastrado");
                });


        }

        private async Task<bool> ProfessorNaoAluno(List<int> ids, CancellationToken cancellationToken)
        {
            return !ids.Contains(IdProfessor);
        }

        private async Task<bool> PessoaExists(List<int> ids, CancellationToken cancellationToken)
        {
            foreach (var id in ids)
            {
                var pessoaExists = await _pessoaRepository.Get(id, cancellationToken);
                if (pessoaExists == null)
                {
                    return false;
                }
            }

            return true;
        }


        private async Task<bool> TurmaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            // Check database for CPF existence
            var turmaExists = await _turmaRepository.Get(id, cancellationToken);
            IdProfessor = turmaExists.IdProfessor;
            return turmaExists != null;
        }
    }
}
