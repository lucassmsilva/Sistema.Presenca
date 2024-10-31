using FluentValidation;
using Sistema.Core.Dominio.Repositories;
using System.Text.RegularExpressions;
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


            RuleFor(x => x.Email)
             .NotEmpty()
             .WithMessage("O email é obrigatório")
             .EmailAddress()
             .WithMessage("Email inválido");

            RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage("O telefone é obrigatório")
            .Must(TelefoneValido)
            .WithMessage("Número de telefone inválido para o Brasil");

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

        private bool TelefoneValido(string telefone)
        {
            // Remove todos os caracteres não numéricos
            string numeroLimpo = Regex.Replace(telefone, @"[^\d]", "");

            // Verifica se o número tem 10 ou 11 dígitos (com DDD)
            if (numeroLimpo.Length != 10 && numeroLimpo.Length != 11)
                return false;

            // Verifica o DDD (assumindo que todos os DDDs válidos estão entre 11 e 99)
            if (int.Parse(numeroLimpo.Substring(0, 2)) < 11)
                return false;

            // Verifica se é celular (começa com 9) quando tem 11 dígitos
            if (numeroLimpo.Length == 11 && numeroLimpo[2] != '9')
                return false;

            // Verifica se não é um número todo igual
            if (new Regex(@"^(\d)\1+$").IsMatch(numeroLimpo))
                return false;

            return true;
        }
    }
}
