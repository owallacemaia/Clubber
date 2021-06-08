using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Club.Data.Repository
{
    public class IntegranteRepository : Repository<Integrante>, IIntegranteRepository
    {
        public IntegranteRepository(ClubberContext context) : base(context) { }

        public async Task<IEnumerable<Integrante>> ObterGruposParticipante(Guid usuarioId)
        {
            return await Db.Integrantes.AsNoTracking().Where(a => a.UsuarioId == usuarioId).ToListAsync();
        }
    }
}
