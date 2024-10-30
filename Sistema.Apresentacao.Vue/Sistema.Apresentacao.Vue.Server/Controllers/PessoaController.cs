using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sistema.Core.Aplicacao.UseCases.Pessoa;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;

namespace Sistema.Apresentacao.Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : Controller
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IValidator<CriarPessoaCommand> _validator;

        public PessoaController(IPessoaRepository pessoaRepository, IUnityOfWork unityOfWork, IValidator<CriarPessoaCommand> validator)
        {
            _pessoaRepository = pessoaRepository;
            _unityOfWork = unityOfWork;
            _validator = validator;
        }


        [HttpPost("create", Name = "CreateUser")]
        public async Task<IActionResult> Create([FromBody]CriarPessoaCommand command, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var pessoa = new Pessoa(command.Nome, command.CPF, command.DataNascimento);

            _pessoaRepository.Create(pessoa);
            await _unityOfWork.Commit(cancellationToken);

            return Ok("Created");
        }
    }
}
