using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Club.Data.Repository
{
    public class SeguidoresRepository : Repository<Seguidor>, ISeguidoresRepository
    {
        public SeguidoresRepository(ClubberContext context) : base(context) { }

        public async Task<IEnumerable<IEnumerable<Seguidor>>> ObterSeguidores()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IEnumerable<Seguidor>>> ObterSeguindo()
        {
            throw new NotImplementedException();
        }
    }
}
