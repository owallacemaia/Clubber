using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Club.WebApi.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(6, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Descricao { get; set; }

        public string Conteudo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataPublicacao { get; set; }

        [HiddenInput]
        public Guid UsuarioId { get; set; }
        [HiddenInput]
        public Guid GrupoId { get; set; }
    }

    public class PostFeedViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(6, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres")]
        public string Descricao { get; set; }

        public string Conteudo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataPublicacao { get; set; }

        [HiddenInput]
        public Guid UsuarioId { get; set; }
    }
}
