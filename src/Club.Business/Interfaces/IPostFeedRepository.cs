using Club.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Club.Business.Interfaces
{
    public interface IPostFeedRepository : IRepository<PostFeed>
    {
        Task<PostFeed> ObterPostUsuario(Guid usuarioId);
        Task<IEnumerable<PostFeed>> ObterTodosPostsUsuario();
    }
}
