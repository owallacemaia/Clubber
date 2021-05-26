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
}
