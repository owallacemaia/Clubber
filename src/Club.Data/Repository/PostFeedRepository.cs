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
}
