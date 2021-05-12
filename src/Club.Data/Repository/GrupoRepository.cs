using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Data.Repository
{
    public class GrupoRepository : Repository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(ClubberContext context) : base(context) { }
        public async Task<IEnumerable<Grupo>> ObterGruposUsuario()
        {
            return await Db.Grupos.AsNoTracking()
                .Include(a => a.Usuario)
                .OrderBy(a => a.Nome)
                .ToListAsync();
        }

        public async Task<Grupo> ObterGrupoUsuario(Guid usuarioId)
        {
            return await Db.Grupos.AsNoTracking()
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
        }
    }

    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ClubberContext context) : base(context) { }

        public async Task<Post> ObterPostUsuario(Guid usuarioId)
        {
            return await Db.Posts.AsNoTracking()
                .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<Post>> ObterTodosPostsUsuario()
        {
            return await Db.Posts.AsNoTracking()
                .OrderBy(a => a.DataPublicacao)
                .ToListAsync();
        }
    }

    public class PostFeedRepository : Repository<PostFeed>, IPostFeedRepository
    {
        public PostFeedRepository(ClubberContext context) : base(context) { }

        public async Task<PostFeed> ObterPostUsuario(Guid usuarioId)
        {
            return await Db.PostsFeed.AsNoTracking()
                .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<PostFeed>> ObterTodosPostsUsuario()
        {
            return await Db.PostsFeed.AsNoTracking()
                .OrderBy(a => a.DataPublicacao)
                .ToListAsync();
        }
    }

    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ClubberContext context) : base(context) { }

    }

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
