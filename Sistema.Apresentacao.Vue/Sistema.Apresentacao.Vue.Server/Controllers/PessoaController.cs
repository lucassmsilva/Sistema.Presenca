using Microsoft.AspNetCore.Mvc;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;

namespace Sistema.Apresentacao.Vue.Server.Controllers
{
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
        public IActionResult Create(string nome, string cpf, DateTime? dtNasc, CancellationToken cancellationToken)
        {
            _pessoaRepository.Create(new Pessoa(nome, cpf, dtNasc));

            _unityOfWork.Commit(cancellationToken);

            return Ok("Created");
        }
    }
}
