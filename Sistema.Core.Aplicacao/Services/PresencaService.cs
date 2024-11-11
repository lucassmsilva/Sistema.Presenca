using FluentValidation;

using Sistema.Core.Aplicacao.UseCases.Presenca;
using Sistema.Core.Dominio.DTO.Presenca;
using Sistema.Core.Dominio.Interfaces;
using Sistema.Core.Dominio.Repositories;

namespace Sistema.Core.Aplicacao.Services
{
    public class PresencaService<T> : IPresencaService<T> where T : IPresenca
    {
        private readonly IPresencaRepository<T> _presencaRepository;

        public PresencaService(IPresencaRepository<T> presencaRepository)
        {
            _presencaRepository = presencaRepository;
        }

        public async Task RegistrarPresenca(IEnumerable<T> lista)
        {
            foreach (var item in lista)
            {
                await _presencaRepository.RegistrarPresenca(item.IdPessoa, item.IdTurmaHorario);
            }
        }

        public async Task RegistrarPresenca(T item)
        {
            await _presencaRepository.RegistrarPresenca(item.IdPessoa, item.IdTurmaHorario);
        }

        public async Task CancelarPresenca(IEnumerable<T> lista)
        {
            foreach (var item in lista)
            {
                await _presencaRepository.CancelarPresenca(item.IdPessoa, item.IdTurmaHorario);
            }
        }

        public async Task CancelarPresenca(T item)
        {
            await _presencaRepository.CancelarPresenca(item.IdPessoa, item.IdTurmaHorario);
        }

        // Obter registros de presença por intervalo de datas
        public async Task<IEnumerable<T>> ObterRegistrosPresenca(int idTurma)
        {
            return await _presencaRepository.ObterRegistrosPresenca(idTurma);
        }
    }
}
