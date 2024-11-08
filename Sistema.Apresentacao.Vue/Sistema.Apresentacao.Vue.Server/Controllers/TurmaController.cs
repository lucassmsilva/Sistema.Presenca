using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sistema.Core.Aplicacao.UseCases.Turma;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.DTO.Turma;
using Sistema.Core.Dominio.Repositories;
using Sistema.Core.Aplicacao.UseCases.TurmaAluno;
using Sistema.Infraestrutura.Persistencia.Repositories;
using Sistema.Core.Dominio.DTO.Pessoa;

namespace Sistema.Apresentacao.Vue.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : Controller
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IValidator<CriarTurmaCommand> _createValidator;
        private readonly IValidator<AtualizarTurmaCommand> _updateValidator;
        private readonly IValidator<CriarTurmaAlunoCommand> _adicionarAlunoValidator;

        public TurmaController(ITurmaRepository turmaRepository, IPessoaRepository pessoaRepository, IUnityOfWork unityOfWork, IValidator<CriarTurmaCommand> createValidator,
            IValidator<AtualizarTurmaCommand> updateValidator, IValidator<CriarTurmaAlunoCommand> adicionarAlunoValidator)
        {
            _turmaRepository = turmaRepository;
            _pessoaRepository = pessoaRepository;
            _unityOfWork = unityOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _adicionarAlunoValidator = adicionarAlunoValidator;
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

            var dtos = await _turmaRepository.Selecionar(t => t.Id == turma.Id, cancellationToken);
            var dto = dtos.Single();

            return Ok(dto);
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string? nome, CancellationToken cancellationToken)
        {
            var results = await _turmaRepository
                .Selecionar(
                p => (string.IsNullOrEmpty(nome) || p.NomeTurma.Contains(nome)),
                cancellationToken);

            return Ok(results);
        }

        [HttpPut("update/{id}")]
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

            var dtos = await _turmaRepository.Selecionar(t => t.Id == turma.Id, cancellationToken);
            var dto = dtos.Single();

            return Ok(dto);
        }

        [HttpDelete("delete/{id}")]
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

        [HttpPost("adicionar-aluno")]
        public async Task<IActionResult> AdicionarAluno([FromBody] CriarTurmaAlunoCommand command, CancellationToken cancellationToken)
        {
            // Validação do comando
            var validationResult = await _adicionarAlunoValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Busca a turma e a pessoa para garantir que ambos existem
            var turma = await _turmaRepository.Get(command.IdTurma, cancellationToken);

            if (turma == null) return NotFound("Turma não encontrada.");

            foreach (int idPessoa in command.Pessoas)
            {
                var pessoa = await _pessoaRepository.Get(idPessoa, cancellationToken);
                if (pessoa != null)
                {
                    turma.Alunos.Add(pessoa);
                }
            }

            // Salva as mudanças usando o Unit of Work
            await _unityOfWork.Commit(cancellationToken);

            var dtos = await _turmaRepository.Selecionar(t => t.Id == turma.Id, cancellationToken);
            var dto = dtos.Single();

            return Ok(dto);
        }

        [HttpPost("sincronizar-alunos")]
        public async Task<IActionResult> SincronizarAlunos([FromBody] CriarTurmaAlunoCommand command, CancellationToken cancellationToken)
        {
            // Validação do comando
            var validationResult = await _adicionarAlunoValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Busca a turma para garantir que existe
            var turma = await _turmaRepository.Get(command.IdTurma, cancellationToken);
            if (turma == null)
                return NotFound("Turma não encontrada.");

            // Buscar todas as pessoas que estão no comando
            var pessoasExistentes = await _pessoaRepository.Search(p => command.Pessoas.Contains(p.Id), cancellationToken);

            // Filtrar IDs de pessoas válidas (existentes no sistema)
            var pessoasIds = pessoasExistentes.Select(p => p.Id).ToList();

            // Adicionar alunos que estão na lista, mas não estão na turma
            var alunosParaAdicionar = pessoasExistentes.Where(p => !turma.Alunos.Any(a => a.Id == p.Id)).ToList();
            foreach (var aluno in alunosParaAdicionar)
            {
                turma.Alunos.Add(aluno);
            }

            // Remover alunos que estão na turma, mas não estão na lista
            var alunosParaRemover = turma.Alunos.Where(a => !pessoasIds.Contains(a.Id)).ToList();
            foreach (var aluno in alunosParaRemover)
            {
                turma.Alunos.Remove(aluno);
            }

            // Salva as mudanças usando o Unit of Work
            await _unityOfWork.Commit(cancellationToken);

            // Retorna os dados da turma atualizada
            var dtos = await _turmaRepository.Selecionar(t => t.Id == turma.Id, cancellationToken);
            var dto = dtos.Single();

            return Ok(dto);
        }

        [HttpPost("remover-aluno")]
        public async Task<IActionResult> RemoverAluno([FromBody] CriarTurmaAlunoCommand command, CancellationToken cancellationToken)
        {
            // Validação do comando
            var validationResult = await _adicionarAlunoValidator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Busca a turma para garantir que ela existe
            var turma = await _turmaRepository.Get(command.IdTurma, cancellationToken);
            if (turma == null)
                return Ok(new List<TurmaDTO>());

            // Remover alunos que estão na lista de IDs enviados
            foreach (int idPessoa in command.Pessoas)
            {
                var alunoParaRemover = turma.Alunos.FirstOrDefault(a => a.Id == idPessoa);
                if (alunoParaRemover != null)
                {
                    turma.Alunos.Remove(alunoParaRemover);
                }
            }

            // Salva as mudanças usando o Unit of Work
            await _unityOfWork.Commit(cancellationToken);

            // Retorna os dados atualizados da turma
            var dtos = await _turmaRepository.Selecionar(t => t.Id == turma.Id, cancellationToken);
            var dto = dtos.Single();

            return Ok(dto);
        }

        [HttpGet("buscar-turmas-aluno/{alunoId}")]

        public async Task<IActionResult> ObterTurmasPorAluno(int alunoId, CancellationToken cancellationToken)
        {
            // Busca as turmas onde o aluno está presente
            var turmasComAluno = await _turmaRepository
                .Selecionar(t => t.Alunos.Any(a => a.Id == alunoId), cancellationToken);
                


            if (!turmasComAluno.Any())
                return NotFound("Nenhuma turma encontrada para este aluno.");

            var listaSemDadosAlunos = turmasComAluno.Select(t => new
            {
                t.Id,
                t.NomeTurma,
                t.IdProfessor,
                t.Professor
            }).ToList();

            return Ok(listaSemDadosAlunos);
        }


    }
}
