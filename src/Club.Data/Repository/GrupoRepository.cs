﻿using Club.Business.Interfaces;
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
}