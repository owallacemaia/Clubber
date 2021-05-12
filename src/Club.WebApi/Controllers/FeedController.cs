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
    [Route("api/feed")]
    public class FeedController : MainController
    {
        private readonly IPostFeedRepository _postFeedRepository;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public FeedController(IPostFeedRepository postFeedRepository,
            IPostService postService,
            IMapper mapper,
            INotificador notificador,
            IUser user,
            IUsuarioRepository usuarioRepository) : base(notificador, user)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _postService = postService;
            _postFeedRepository = postFeedRepository;
        }

        //FEED

        [HttpGet]
        public async Task<IEnumerable<PostFeedViewModel>> ObterTodosFeed()
        {
            var posts = _mapper.Map<IEnumerable<PostFeedViewModel>>(await _postFeedRepository.ObterTodos());

            return (posts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PostFeedViewModel>> ObterFeedPorId(Guid id)
        {
            var posts = _mapper.Map<PostFeedViewModel>(await _postFeedRepository.ObterPorId(id));

            if (posts == null)
            {
                NotificarErro("O Grupo não foi encotrado");
                return CustomResponse(posts);
            }

            return CustomResponse(posts);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<PostFeedViewModel>> AdicionarFeed(PostFeedViewModel postFeedViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var usuario = await _usuarioRepository.ObterPorId(AppUser.GetUserId());

            if (!AppUser.IsAuthenticated() || usuario == null)
            {
                NotificarErro("Você precisa estar autenticado");
                return CustomResponse(postFeedViewModel);
            }

            var post = _mapper.Map<PostFeed>(postFeedViewModel);
            post.UsuarioId = usuario.Id;

            await _postService.AdicionarFeed(post);

            return CustomResponse(post);
        }

        [HttpPut("atualizar/{id:guid}")]
        public async Task<ActionResult<PostFeedViewModel>> AtualizarFeed(Guid id, PostFeedViewModel postFeedViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var usuario = await _usuarioRepository.ObterPorId(AppUser.GetUserId());

            if (!AppUser.IsAuthenticated() || usuario == null)
            {
                NotificarErro("Você precisa estar autenticado");
                return CustomResponse(postFeedViewModel);
            }

            if (id != postFeedViewModel.Id)
            {
                NotificarErro("Houve algum problema ao editar o grupo");
                return CustomResponse(postFeedViewModel);
            }

            

            var post = _mapper.Map<PostFeed>(postFeedViewModel);
            post.UsuarioId = usuario.Id;

            await _postService.AtualizarFeed(post);

            return CustomResponse(post);
        }

        [HttpDelete("excluir/{id:guid}")]
        public async Task<ActionResult<PostFeedViewModel>> ExcluirFeed(Guid id)
        {
            var post = await _postFeedRepository.ObterPorId(id);

            if (post == null)
            {
                NotificarErro("Houve um problema ao tentar remover o grupo!");
                return CustomResponse();
            }

            await _postService.RemoverFeed(id);

            return CustomResponse();
        }
    }

    [Route("api/seguidor")]
    public class SeguidorController : MainController
    {
        private readonly ISeguidoresRepository _seguidoresRepository;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public SeguidorController(ISeguidoresRepository seguidoresRepository,
            IMapper mapper,
            INotificador notificador,
            IUser user,
            IUsuarioRepository usuarioRepository) : base(notificador, user)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _seguidoresRepository = seguidoresRepository;
        }

        //[HttpGet("seguindo/{id:guid}")]
        //public async Task<IEnumerable<Seguidor>> Seguindo(Guid id)
        //{
        //    //var seguindo = await _seguidoresRepository.ObterSeguidores(id);
        //}
    }
}
