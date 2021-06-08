using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Business.Models
{
    public class Usuario : Entity
    {
        public string NomeUsuario { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Celular { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Biografia { get; set; }
        public string Site { get; set; }

        public Usuario() {}

        public Usuario(Guid id, string nomeUsuario, string nome, DateTime dataNascimento, string celular)
        {
            Id = id;
            NomeUsuario = nomeUsuario;
            Nome = nome;
            DataNascimento = dataNascimento;
            Celular = celular;
        }

        public IEnumerable<Grupo> Grupos { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<PostFeed> PostsFeed { get; set; }
        public IEnumerable<Seguidor> Seguidor { get; set; }
        public IEnumerable<Integrante> Participados { get; set; }
    }
}
