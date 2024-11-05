using FluentValidation;
using Sistema.Core.Dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Core.Aplicacao.UseCases.Turma
{
    public class AtualizarTurmaCommandValidator : AbstractValidator<AtualizarTurmaCommand>
    {
        public readonly IPessoaRepository _pessoaRepository;

        public AtualizarTurmaCommandValidator(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;

            RuleFor(x => x.IdProfessor)
                .NotEmpty()
                .MustAsync(PessoaExists)
                .WithMessage("Professor não cadastrado");

        }

        private async Task<bool> PessoaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            // Check database for ID existence
            var pessoaExists = await _pessoaRepository.Get(id, cancellationToken);
            return pessoaExists != null;
        }

    }
}
