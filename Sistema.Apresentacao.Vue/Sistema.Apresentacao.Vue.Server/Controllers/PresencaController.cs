using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using Sistema.Core.Aplicacao.Services;
using Sistema.Core.Aplicacao.UseCases.Presenca;
using Sistema.Core.Dominio.DTO.Presenca;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Repositories;
using Sistema.Infraestrutura.Persistencia.Migrations;

namespace Sistema.Apresentacao.Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : Controller
    {
        private readonly IPresencaRepository<PresencaDTO> _presencaRepository;
        private readonly PresencaService<PresencaDTO> _service;
        private readonly IValidator<PresencaCommand> _validator;
        private readonly IUnityOfWork _unityOfWork;

        public PresencaController(
            IPresencaRepository<PresencaDTO> presencaRepository,
            IUnityOfWork unityOfWork,
            PresencaService<PresencaDTO> service,
            IValidator<PresencaCommand> validator)
        {
            _presencaRepository = presencaRepository;
            _service = service;
            _validator = validator;
            _unityOfWork = unityOfWork;
        }

        [HttpGet("obter-registros-presenca")]
        public async Task<IActionResult> ObterRegistrosPresenca(int idTurma, int idTurmaHorario, CancellationToken cancellationToken)
        {
            if (idTurma == 0)
            {
                return BadRequest("Turma inválida");
            }

            var registros = await _service.ObterRegistrosPresenca(idTurma);
            if (idTurmaHorario > 0 && registros.Any())
            {
                registros = registros.Where(item => item.IdTurmaHorario == idTurmaHorario).ToList();
            }
            return Ok(registros);
        }

        [HttpPost("registrar-presenca")]
        public async Task<IActionResult> RegistrarPresenca([FromBody] PresencaCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var presencaDTO = new PresencaDTO
                {
                    IdPessoa = command.IdPessoa,
                    IdTurmaHorario = command.IdTurmaHorario
                };

                await _service.RegistrarPresenca(presencaDTO);
                await _unityOfWork.Commit(cancellationToken);

                return Ok("Presença registrada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao registrar presença: {ex.Message}");
            }
        }

        [HttpPost("registrar-presenca-lista")]
        public async Task<IActionResult> RegistrarPresencaLista([FromBody] IEnumerable<PresencaCommand> commands, CancellationToken cancellationToken)
        {
            // Valida todos os commands da lista
            foreach (var command in commands)
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
            }

            try
            {
                var presencasDTO = commands.Select(c => new PresencaDTO
                {
                    IdPessoa = c.IdPessoa,
                    IdTurmaHorario = c.IdTurmaHorario
                });

                await _service.RegistrarPresenca(presencasDTO);
                await _unityOfWork.Commit(cancellationToken);

                return Ok("Presenças registradas com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao registrar presenças: {ex.Message}");
            }
        }

        [HttpPost("cancelar-presenca")]
        public async Task<IActionResult> CancelarPresenca([FromBody] PresencaCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var presencaDTO = new PresencaDTO
                {
                    IdPessoa = command.IdPessoa,
                    IdTurmaHorario = command.IdTurmaHorario
                };

                await _service.CancelarPresenca(presencaDTO);
                await _unityOfWork.Commit(cancellationToken);

                return Ok("Presença cancelada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cancelar presença: {ex.Message}");
            }
        }

        [HttpPost("cancelar-presenca-lista")]
        public async Task<IActionResult> CancelarPresencaLista([FromBody] IEnumerable<PresencaCommand> commands, CancellationToken cancellationToken)
        {
            foreach (var command in commands)
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
            }

            try
            {
                var presencasDTO = commands.Select(c => new PresencaDTO
                {
                    IdPessoa = c.IdPessoa,
                    IdTurmaHorario = c.IdTurmaHorario
                });

                await _service.CancelarPresenca(presencasDTO);
                await _unityOfWork.Commit(cancellationToken);

                return Ok("Presenças canceladas com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cancelar presenças: {ex.Message}");
            }
        }
    }
}