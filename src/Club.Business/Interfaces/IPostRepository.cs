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

    public interface IPostFeedRepository : IRepository<PostFeed>
    {
        Task<PostFeed> ObterPostUsuario(Guid usuarioId);
        Task<IEnumerable<PostFeed>> ObterTodosPostsUsuario();
    }

    public interface IGrupoRepository : IRepository<Grupo>
    {
        Task<IEnumerable<Grupo>> ObterGruposUsuario();
        Task<Grupo> ObterGrupoUsuario(Guid usuarioId);
    }

    public interface ISeguidoresRepository : IRepository<Seguidor>
    {
        Task<IEnumerable<IEnumerable<Seguidor>>> ObterSeguidores();
        Task<IEnumerable<IEnumerable<Seguidor>>> ObterSeguindo();
    }
    public interface IUsuarioRepository : IRepository<Usuario>
    {
    }
}
