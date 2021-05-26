using Club.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Business.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> ObterPostUsuario(Guid usuarioId);
        Task<IEnumerable<Post>> ObterTodosPostsUsuario(); 
    }
}
