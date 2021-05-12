using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Business.Models.Validation;
using System;
using System.Threading.Tasks;

namespace Club.Business.Services
{
    public interface IGrupoService : IDisposable
    {
        Task<bool> Adicionar(Grupo grupo);
        Task<bool> Atualizar(Grupo grupo);
        Task<bool> Remover(Guid grupoId);
    }

    public class GrupoService : BaseService, IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository grupoRepository, INotificador notificador) : base(notificador)
        {
            _grupoRepository = grupoRepository;
        }

        public async Task<bool> Adicionar(Grupo grupo)
        {
            if (!ExecutarValidacao(new GrupoValidation(), grupo)) return false;

            await _grupoRepository.Adicionar(grupo);
            return true;
        }

        public async Task<bool> Atualizar(Grupo grupo)
        {
            if (!ExecutarValidacao(new GrupoValidation(), grupo)) return false;

            await _grupoRepository.Atualizar(grupo);
            return true;
        }

        public async Task<bool> Remover(Guid grupoId)
        {
            await _grupoRepository.Remover(grupoId);
            return true;
        }

        public void Dispose()
        {
            _grupoRepository?.Dispose();
        }
    }
}
