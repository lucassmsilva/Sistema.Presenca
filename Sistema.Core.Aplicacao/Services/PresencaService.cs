using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Models;
using Sistema.Core.Dominio.Repositories;

namespace Sistema.Core.Aplicacao.Services
{
    public class PresencaService : IPresencaService
    {
        private readonly IPresencaRepository _presencaRepository;
        
        public PresencaService(IPresencaRepository presencaRepository, IUnityOfWork unityOfWork)
        {
            _presencaRepository = presencaRepository;
        }

        public bool RegistrarPresenca(PresencaModel presenca)
        {
            _presencaRepository.Create(presenca);

            return true;

        }

        public bool CancelarPresenca(PessoaModel pessoa)
        {
            // Implement logic to cancel a person's presence
            // Use _presencaRepository and _unityOfWork as needed
            throw new NotImplementedException();
        }

        public IEnumerable<PresencaModel> ObterRegistrosPresenca(DateTime dataInicial, DateTime dataFinal)
        {
            // Implement logic to retrieve presence records within a date range
            // Use _presencaRepository to fetch the data
            throw new NotImplementedException();
        }

        bool IPresencaService.RegistrarPresenca<T>(IEnumerable<T> pessoas)
        {
            throw new NotImplementedException();
        }

        bool IPresencaService.RegistrarPresenca<T>(T pessoa)
        {
            throw new NotImplementedException();
        }

        bool IPresencaService.CancelarPresenca<T>(IEnumerable<T> pessoas)
        {
            throw new NotImplementedException();
        }

        bool IPresencaService.CancelarPresenca<T>(T pessoa)
        {
            throw new NotImplementedException();
        }
    }
}