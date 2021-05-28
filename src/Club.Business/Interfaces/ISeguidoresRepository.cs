using Club.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Club.Business.Interfaces
{
    public interface ISeguidoresRepository : IRepository<Seguidor>
    {
        Task<IEnumerable<IEnumerable<Seguidor>>> ObterSeguidores();
        Task<IEnumerable<IEnumerable<Seguidor>>> ObterSeguindo();
    }
}
