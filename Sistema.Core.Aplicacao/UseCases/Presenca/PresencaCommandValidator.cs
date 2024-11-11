using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.Presenca
{
    public class PresencaCommandValidator : AbstractValidator<PresencaCommand>
    {
        public PresencaCommandValidator()
        {
            RuleFor(x => x.IdPessoa)
                .NotEmpty()
                .WithMessage("O ID da pessoa é obrigatório")
                .GreaterThan(0)
                .WithMessage("O ID da pessoa deve ser maior que zero");

            RuleFor(x => x.IdTurmaHorario)
                .NotEmpty()
                .WithMessage("O ID da turma/horário é obrigatório")
                .GreaterThan(0)
                .WithMessage("O ID da turma/horário deve ser maior que zero");
        }
    }
}
