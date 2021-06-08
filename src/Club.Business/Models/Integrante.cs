using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Business.Models
{
    public class Integrante : Entity
    {
        public Guid UsuarioId { get; set; }
        public Guid GrupoId { get; set; }

        public Usuario Usuario { get; set; }
    }
}
