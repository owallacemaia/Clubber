using System;

namespace Club.Business.Models
{
    public class Seguidor : Entity
    {
        public Guid UsuarioId { get; set; }
        public Guid SeguidorId { get; set; }
    }
}