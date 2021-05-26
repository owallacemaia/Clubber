using Club.Business.Models;
using System;
using System.Threading.Tasks;

namespace Club.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterUsuarioPosts(Guid id);
        Task<Usuario> ObterUsuarioGrupos(Guid id);
    }
}
