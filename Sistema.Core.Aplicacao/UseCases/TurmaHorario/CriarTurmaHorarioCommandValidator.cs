using FluentValidation;
using Sistema.Core.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.TurmaHorario
{
    public class CriarTurmaHorarioCommandValidator : AbstractValidator<CriarTurmaHorarioCommand>
    {
        public readonly ITurmaRepository _turmaRepository;

        public CriarTurmaHorarioCommandValidator(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;

            RuleFor(x => x.IdTurma)
                .MustAsync(TurmaExists)
                .WithMessage("Professor não cadastrado");
        }

        private async Task<bool> TurmaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            var pessoaExists = await _turmaRepository.Get(id, cancellationToken);
            return pessoaExists != null;
        }

    }

}
