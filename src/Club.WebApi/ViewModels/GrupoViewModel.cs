using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Club.WebApi.ViewModels
{
    public class GrupoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(300, ErrorMessage = "o campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public string ImagemCapa { get; set; }

        public int Tipo { get; set; }

        public Guid UsuarioId { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public IEnumerable<PostViewModel> Posts { get; set; }
    }
}
