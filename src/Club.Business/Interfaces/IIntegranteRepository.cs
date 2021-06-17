using Club.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Club.Business.Interfaces
{
    public interface IIntegranteRepository : IRepository<Integrante>
    {
        Task<IEnumerable<Integrante>> ObterGruposParticipante(Guid usuarioId);
        Task<Integrante> ObterIntegranteExistente(Guid usuarioId, Guid grupoId);
    }
}
