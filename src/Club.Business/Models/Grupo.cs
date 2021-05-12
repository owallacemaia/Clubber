using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Business.Models
{
    public class Grupo : Entity
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public string ImagemCapa { get; set; }
        public TipoGrupo Tipo { get; set; }


        public Usuario Usuario { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
