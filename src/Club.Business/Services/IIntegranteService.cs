using Club.Business.Interfaces;
using Club.Business.Models;
using System;
using System.Threading.Tasks;

namespace Club.Business.Services
{
    public interface IIntegranteService : IDisposable
    {
        Task<bool> Adicionar(Integrante integrante);
        Task<bool> Remover(Guid id);
    }

    public class IntegranteService : BaseService, IIntegranteService
    {
        private readonly IIntegranteRepository _integranteRepository;
        private readonly IGrupoRepository _grupoRepository;

        public IntegranteService(IIntegranteRepository integranteRepository, INotificador notificador, IGrupoRepository grupoRepository) : base (notificador)
        {
            _integranteRepository = integranteRepository;
            _grupoRepository = grupoRepository;
        }

        public async Task<bool> Adicionar(Integrante integrante)
        {
            var grupo = await _grupoRepository.ObterPorId(integrante.GrupoId);

            if (grupo.Id != integrante.GrupoId) return false;

            var integrateExistente = await _integranteRepository.ObterIntegranteExistente(integrante.UsuarioId, integrante.GrupoId);

            if (integrateExistente != null)
            {
                Notificar("Usuario já está no grupo");
                return false;
            }

            await _integranteRepository.Adicionar(integrante);

            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            var grupo = await _grupoRepository.ObterPorId(id);

            if (grupo == null) return false;

            await _integranteRepository.Remover(grupo.Id);

            return true;
        }

        public void Dispose()
        {
            _integranteRepository?.Dispose();
        }
    }
}
