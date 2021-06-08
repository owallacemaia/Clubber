using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Club.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ClubberContext context) : base(context) { }

        public async Task<Usuario> ObterUsuarioGrupos(Guid id)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(a => a.Posts)
                .Include(a => a.Grupos)
                .OrderBy(a => a.Nome)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Usuario> ObterUsuarioPosts(Guid id)
        {
            return await Db.Usuarios.AsNoTracking()
                .Include(a => a.Posts)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
