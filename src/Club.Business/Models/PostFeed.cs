using System;

namespace Club.Business.Models
{
    public class PostFeed : Entity
    {
        public Guid UsuarioId { get; set; }
        public string Descricao { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public Usuario Usuario { get; set; }
    }
}
