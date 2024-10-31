﻿using FluentValidation;
using Sistema.Core.Dominio.Repositories;
namespace Sistema.Core.Aplicacao.UseCases.Pessoa
{
    public class CriarPessoaCommandValidator : AbstractValidator<CriarPessoaCommand>
    {

        private readonly IPessoaRepository _pessoaRepository;

        public CriarPessoaCommandValidator(IPessoaRepository pessoaRepository)
        {

             _pessoaRepository = pessoaRepository;

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MinimumLength(3)
                .WithMessage("O nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100)
                .WithMessage("O nome deve ter no máximo 100 caracteres");


            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("O CPF é obrigatório")
                .Must(BeValidCPF)
                .WithMessage("CPF inválido")
                .MustAsync(BeUniqueCPF)
                .WithMessage("CPF já cadastrado");

            RuleFor(x => x.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória")
                .Must(BeValidBirthDate)
                .WithMessage("A data de nascimento não pode ser futura");
        }

        private bool BeValidCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11) return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.Distinct().Count() == 1) return false;

            return true; // Adicione aqui a lógica completa de validação de CPF
        }

        private async Task<bool> BeUniqueCPF(string cpf, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(cpf)) return false;
            
            // Check database for CPF existence
            var pessoaExists = await _pessoaRepository.CPFExistsAsync(cpf);
            return !pessoaExists;
        }

        private bool BeValidBirthDate(DateTime birthDate)
        {
            return birthDate <= DateTime.Now;
        }
    }
}