using AutoMapper;
using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Business.Services;
using Club.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Club.WebApi.Controllers
{
    [Route("api/posts")]
    public class PostController : MainController
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public PostController(IPostRepository postRepository,
            IPostService postService,
            IMapper mapper,
            INotificador notificador,
            IUser user,
            IUsuarioRepository usuarioRepository) : base(notificador, user)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _postRepository = postRepository;
            _postService = postService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostViewModel>> ObterTodos()
        {
            var posts = _mapper.Map<IEnumerable<PostViewModel>>(await _postRepository.ObterTodos());

            return (posts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostViewModel>> ObterPorId(Guid id)
        {
            var posts = _mapper.Map<PostViewModel>(await _postRepository.ObterPorId(id));

            if (posts == null)
            {
                NotificarErro("O Grupo não foi encotrado");
                return CustomResponse(posts);
            }

            return CustomResponse(posts);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<PostViewModel>> Adicionar(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!AppUser.IsAuthenticated())
            {
                NotificarErro("Você precisa estar autenticado");
                return CustomResponse(postViewModel);
            }

            var usuario = await _usuarioRepository.ObterPorId(AppUser.GetUserId());

            var post = _mapper.Map<Post>(postViewModel);
            post.UsuarioId = usuario.Id;

            await _postService.Adicionar(post);

            return CustomResponse(post);
        }

        [HttpPut("atualizar/{id:guid}")]
        public async Task<ActionResult<PostViewModel>> Atualizar(Guid id, PostViewModel postViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!AppUser.IsAuthenticated())
            {
                NotificarErro("Você precisa estar autenticado");
                return CustomResponse(postViewModel);
            }

            if (id != postViewModel.Id)
            {
                NotificarErro("Houve algum problema ao editar o grupo");
                return CustomResponse(postViewModel);
            }

            var usuario = await _usuarioRepository.ObterPorId(AppUser.GetUserId());

            var post = _mapper.Map<Post>(postViewModel);
            post.UsuarioId = usuario.Id;

            await _postService.Atualizar(post);

            return CustomResponse(post);
        }

        [HttpDelete("excluir/{id:guid}")]
        public async Task<ActionResult<PostViewModel>> Excluir(Guid id)
        {
            var post = await _postRepository.ObterPorId(id);

            if (post == null)
            {
                NotificarErro("Houve um problema ao tentar remover o grupo!");
                return CustomResponse();
            }

            await _postRepository.Remover(id);

            return CustomResponse();
        }

        
    }

}
