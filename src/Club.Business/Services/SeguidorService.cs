using Club.Business.Interfaces;
using Club.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Business.Services
{
    public interface ISeguidorService : IDisposable
    {
        Task<bool> Adicionar(Seguidor seguidor);
        Task<bool> Remover(Guid id);
    }

    public class SeguidorService : BaseService, ISeguidorService
    {
        private readonly ISeguidoresRepository _seguidoresRepository;

        public SeguidorService(INotificador notificador, 
            ISeguidoresRepository seguidoresRepository) : base(notificador)
        {
            _seguidoresRepository = seguidoresRepository;
        }

        public async Task<bool> Adicionar(Seguidor seguidor)
        {
            await _seguidoresRepository.Adicionar(seguidor);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _seguidoresRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
