using FluentValidation;
using Sistema.Core.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.TurmaHorario
{
    public class SyncTurmaHorarioCommandValidator : AbstractValidator<SyncTurmaHorarioCommand>
    {
        public readonly ITurmaRepository _turmaRepository;

        public SyncTurmaHorarioCommandValidator(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;

            RuleFor(x => x.IdTurma)
                .MustAsync(TurmaExists)
                .WithMessage("Turma não cadastrado");

            RuleFor(x => x.IdTurma).GreaterThan(0);
            RuleFor(x => x.HoraInicio)
                .NotEmpty()
                .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$")
                .WithMessage("Hora início deve estar no formato HH:mm");

            RuleFor(x => x.HoraFim)
                .NotEmpty()
                .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$")
                .WithMessage("Hora fim deve estar no formato HH:mm");

            RuleFor(x => x.Datas)
                .NotEmpty()
                .Must(datas => datas.All(d => DateTime.TryParseExact(d, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _)))
                .WithMessage("Datas devem estar no formato dd/MM/yyyy");
        }

        private async Task<bool> TurmaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            var pessoaExists = await _turmaRepository.Get(id, cancellationToken);
            return pessoaExists != null;
        }

    }

}
