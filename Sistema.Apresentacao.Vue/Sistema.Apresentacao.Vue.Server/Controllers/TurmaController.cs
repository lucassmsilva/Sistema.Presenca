using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sistema.Core.Aplicacao.UseCases.Turma;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;

namespace Sistema.Apresentacao.Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : Controller
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IValidator<CriarTurmaCommand> _createValidator;
        private readonly IValidator<AtualizarTurmaCommand> _updateValidator;

        public TurmaController(ITurmaRepository turmaRepository, IUnityOfWork unityOfWork, IValidator<CriarTurmaCommand> createValidator,
            IValidator<AtualizarTurmaCommand> updateValidator)
        {
            _turmaRepository = turmaRepository;
            _unityOfWork = unityOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CriarTurmaCommand command, CancellationToken cancellationToken)
        {

            var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var turma = command.ToTurma();

            _turmaRepository.Create(turma);
            await _unityOfWork.Commit(cancellationToken);

            return Ok("Created");
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string? nome, CancellationToken cancellationToken)
        {
            var results = await _turmaRepository.Search(
                p => (string.IsNullOrEmpty(nome) || p.NomeTurma.Contains(nome)),
                cancellationToken);

            var list = new List<TurmaDTO>();

            foreach (var result in results)
            {
                list.Add(TurmaDTO.FromEntity(result));
            }

            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AtualizarTurmaCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("ID inválido");

            var turma = await _turmaRepository.Get(id, cancellationToken);
            if (turma == null)
                return NotFound();

            var validationResult = await _updateValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            turma = command.MapToTurma(turma);   

            _turmaRepository.Update(turma);
            await _unityOfWork.Commit(cancellationToken);

            return Ok(TurmaDTO.FromEntity(turma));
        }

        [HttpDelete("{id}", Name = "DeleteTurma")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var turma = await _turmaRepository.Get(id, cancellationToken);
            if (turma == null)
            {
                return NotFound("Turma não encontrada");
            }

             _turmaRepository.Delete(turma);

            await _unityOfWork.Commit(cancellationToken);
            return Ok("Deleted");
        }
    }
}
