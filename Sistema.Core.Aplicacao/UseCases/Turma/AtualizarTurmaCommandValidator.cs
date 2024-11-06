using FluentValidation;
using Sistema.Core.Aplicacao.Utils;
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
        public readonly ITurmaRepository _turmaRepository;

        public AtualizarTurmaCommandValidator(IPessoaRepository pessoaRepository, ITurmaRepository turmaRepository)
        {
            _pessoaRepository = pessoaRepository;
            _turmaRepository = turmaRepository;

            RuleFor(x => x.IdProfessor)
                .NotEmpty()
                .MustAsync(PessoaExists)
                .WithMessage("Professor não cadastrado");

            RuleFor(x => x.NomeTurma)
            .NotEmpty()
            .WithMessage("O nome é obrigatório")
            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres")
            .MaximumLength(100)
            .WithMessage("O nome deve ter no máximo 100 caracteres");


            RuleFor(x => x.Sigla)
            .NotEmpty()
            .WithMessage("O nome é obrigatório")
            .MinimumLength(3)
            .MustAsync(BeUniqueTurmaSigla)
            .WithMessage("Sigla já cadastrada");

        }

        private async Task<bool> PessoaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            // Check database for ID existence
            var pessoaExists = await _pessoaRepository.Get(id, cancellationToken);
            return pessoaExists != null;
        }



        private async Task<bool> BeUniqueTurmaSigla(AtualizarTurmaCommand turma, string sigla, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(sigla))
                return false;

            // Verifica se existe outra turma com a mesma sigla
            var lstTurmaExistente = await _turmaRepository.Search(p => p.Sigla.Equals(StringUtils.NormalizeName(sigla)), cancellationToken);

            if (lstTurmaExistente.Count > 1)
            {
                return false;
            }

            var turmaExistente = lstTurmaExistente.FirstOrDefault();
            // Retorna verdadeiro se:
            // - Não existe nenhuma turma com a mesma sigla
            // - Ou se existe, mas é o mesmo registro (mesmo Id)
            return turmaExistente == null || turmaExistente.Id == turma.Id;
        }
    }
}
