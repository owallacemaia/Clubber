using System;

namespace Club.Business.Models
{
    public class Integrante : Entity
    {
        public Guid UsuarioId { get; set; }
        public Guid GrupoId { get; set; }

        public Grupo Grupo { get; set; }
    }
}
