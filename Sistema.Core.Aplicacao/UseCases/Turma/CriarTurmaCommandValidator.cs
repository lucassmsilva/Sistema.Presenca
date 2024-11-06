using FluentValidation;
using Sistema.Core.Aplicacao.Utils;
using Sistema.Core.Dominio.Repositories;


namespace Sistema.Core.Aplicacao.UseCases.Turma
{
    public class CriarTurmaCommandValidator : AbstractValidator<CriarTurmaCommand>
    {

        public readonly ITurmaRepository _turmaRepository;
        public readonly IPessoaRepository _pessoaRepository;

        public CriarTurmaCommandValidator( ITurmaRepository turmaRepository,  IPessoaRepository pessoaRepository)
        {
            _turmaRepository = turmaRepository;
            _pessoaRepository = pessoaRepository;

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

            RuleFor(x => x.IdProfessor)
                .NotEmpty()
                .MustAsync(PessoaExists)
                .WithMessage("Professor não cadastrado");

        }

        private async Task<bool> PessoaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            // Check database for CPF existence
            var pessoaExists = await _pessoaRepository.Get(id, cancellationToken);
            return pessoaExists != null;
        }

        
        private async Task<bool> BeUniqueTurmaSigla(string turma, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(turma)) return false;

            // Check database for turma existence
            var pessoaExists = await _turmaRepository.TurmaExistsAsync(StringUtils.NormalizeName(turma));

            return !pessoaExists;
        }
    }
}
