using Microsoft.AspNetCore.Mvc;
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

        public PessoaController(IPessoaRepository pessoaRepository, IUnityOfWork unityOfWork)
        {
            _pessoaRepository = pessoaRepository;
            _unityOfWork = unityOfWork;
        }


        [HttpPost("create", Name = "CreateUser")]
        public IActionResult Create([FromBody]Pessoa pessoa, CancellationToken cancellationToken)
        {
            _pessoaRepository.Create(pessoa);

            _unityOfWork.Commit(cancellationToken);

            return Ok("Created");
        }
    }
}
