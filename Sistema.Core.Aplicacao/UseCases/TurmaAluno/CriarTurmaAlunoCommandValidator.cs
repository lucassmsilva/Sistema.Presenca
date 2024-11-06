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
        public CriarTurmaAlunoCommandValidator( ITurmaRepository turmaRepository,  IPessoaRepository pessoaRepository)
        {
            _turmaRepository = turmaRepository;
            _pessoaRepository = pessoaRepository;
            
            RuleFor(x => x.IdPessoa)
                .NotEmpty()
                .MustAsync(PessoaExists)
                .WithMessage("Professor não cadastrado");

            
            RuleFor(x => x.IdTurma)
                .NotEmpty()
                .MustAsync(TurmaExists)
                .WithMessage("Turma não cadastrado");
        }

        
        private async Task<bool> PessoaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            // Check database for CPF existence
            var pessoaExists = await _pessoaRepository.Get(id, cancellationToken);
            return pessoaExists != null;
        }

        
        private async Task<bool> TurmaExists(int id, CancellationToken cancellationToken)
        {
            if (id == 0) return false;

            // Check database for CPF existence
            var turmaExists = await _turmaRepository.Get(id, cancellationToken);
            return turmaExists != null;
        }
    }
}
