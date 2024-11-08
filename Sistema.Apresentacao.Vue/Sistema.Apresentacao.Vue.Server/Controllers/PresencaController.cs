using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using Sistema.Core.Aplicacao.UseCases.Presenca;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Repositories;

namespace Sistema.Apresentacao.Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : Controller
    {
        private readonly IPresencaRepository _presencaRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IValidator<CriarPresencaCommand> _createValidator;
        private readonly IValidator<AtualizarPresencaCommand> _updateValidator;

        public PresencaController(IPresencaRepository presencaRepository, IUnityOfWork unityOfWork, IValidator<CriarPresencaCommand> createValidator,
            IValidator<AtualizarPresencaCommand> updateValidator)
        {
            _presencaRepository = presencaRepository;
            _unityOfWork = unityOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }


        //[HttpPost("create")]
        //public async Task<IActionResult> Create([FromBody] CriarPresencaCommand command, CancellationToken cancellationToken)
        //{

        //    var validationResult = await _createValidator.ValidateAsync(command, cancellationToken);

        //    if (!validationResult.IsValid)
        //    {
        //        return BadRequest(validationResult.Errors);
        //    }

        //    var presenca = command.ToPresenca();

        //    _presencaRepository.Create(presenca);
        //    await _unityOfWork.Commit(cancellationToken);

        //    return Ok("Created");
        //}


        //[HttpGet("search")]
        //public async Task<IActionResult> Search(string? nome, string? cpf, CancellationToken cancellationToken)
        //{
        //    var results = await _presencaRepository.Search(
        //        p => (string.IsNullOrEmpty(nome) || p.Nome.Contains(nome)) &&
        //             (string.IsNullOrEmpty(cpf) || p.Cpf == cpf),
        //        cancellationToken);

        //    var list = new List<PresencaDTO>();

        //    foreach (var result in results)
        //    {
        //        list.Add(PresencaDTO.FromEntity(result));
        //    }

        //    return Ok(list);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] AtualizarPresencaCommand command, CancellationToken cancellationToken)
        //{
        //    if (id != command.Id)
        //        return BadRequest("ID inválido");

        //    var presenca = await _presencaRepository.Get(id, cancellationToken);
        //    if (presenca == null)
        //        return NotFound();

        //    var validationResult = await _updateValidator.ValidateAsync(command, cancellationToken);

        //    if (!validationResult.IsValid)
        //    {
        //        return BadRequest(validationResult.Errors);
        //    }

        //    presenca = command.MapToPresenca(presenca);   

        //    _presencaRepository.Update(presenca);
        //    await _unityOfWork.Commit(cancellationToken);

        //    return Ok(PresencaDTO.FromEntity(presenca));
        //}

        //[HttpDelete("{id}", Name = "DeletePresenca")]
        //public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        //{
        //    var presenca = await _presencaRepository.Get(id, cancellationToken);
        //    if (presenca == null)
        //    {
        //        return NotFound("Presenca não encontrada");
        //    }

        //     _presencaRepository.Delete(presenca);

        //    await _unityOfWork.Commit(cancellationToken);
        //    return Ok("Deleted");
        //}
    }
}
