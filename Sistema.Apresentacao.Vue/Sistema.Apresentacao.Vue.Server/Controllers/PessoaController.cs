﻿using FluentValidation;
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
        private readonly IValidator<CriarPessoaCommand> _createValidator;
        private readonly IValidator<AtualizarPessoaCommand> _updateValidator;

        public PessoaController(IPessoaRepository pessoaRepository, IUnityOfWork unityOfWork, IValidator<CriarPessoaCommand> createValidator,
            IValidator<AtualizarPessoaCommand> updateValidator)
        {
            _pessoaRepository = pessoaRepository;
            _unityOfWork = unityOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        [HttpPost("create", Name = "CreateUser")]
        public async Task<IActionResult> Create([FromBody] CriarPessoaCommand command, CancellationToken cancellationToken)
        {

            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var pessoa = new Pessoa(command.Nome, command.CPF, command.DataNascimento);

            _pessoaRepository.Create(pessoa);
            await _unityOfWork.Commit(cancellationToken);

            return Ok("Created");
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string? nome, string? cpf, CancellationToken cancellationToken)
        {
            var results = await _pessoaRepository.Search(
                p => (string.IsNullOrEmpty(nome) || p.Nome.Contains(nome)) &&
                     (string.IsNullOrEmpty(cpf) || p.Cpf == cpf),
                cancellationToken);

            var list = new List<PessoaDTO>();

            foreach (var result in results) 
             {
                list.Add(PessoaDTO.FromEntity(result));
             }

            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarPessoaCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("ID inválido");

            var pessoa = await _pessoaRepository.Get(id, cancellationToken);
            if (pessoa == null)
                return NotFound();

            var validationResult = await _updateValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            pessoa.Nome = command.Nome;
            pessoa.Cpf = command.CPF;
            pessoa.DataNascimento = command.DataNascimento;

            _pessoaRepository.Update(pessoa);
            await _unityOfWork.Commit(cancellationToken);

            return Ok(PessoaDTO.FromEntity(pessoa));
        }
    }
}
