using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Business.Models
{
    public class Post : Entity
    {
        public Guid UsuarioId { get; set; }
        public Guid GrupoId { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public Grupo Grupo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
