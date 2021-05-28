using AutoMapper;
using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Business.Services;
using Club.WebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Club.WebApi.Controllers
{
    [Route("api/grupos")]
    public class GrupoController : MainController
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IGrupoService _grupoService;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public GrupoController(IGrupoRepository grupoRepository,
            IGrupoService grupoService,
            IMapper mapper,
            INotificador notificador,
            IUser user,
            IUsuarioRepository usuarioRepository) : base(notificador, user)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _grupoRepository = grupoRepository;
            _grupoService = grupoService;
        }

        [HttpGet]
        public async Task<IEnumerable<GrupoViewModel>> ObterTodos()
        {
            var grupos = _mapper.Map<IEnumerable<GrupoViewModel>>(await _grupoRepository.ObterTodos());

            return (grupos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GrupoViewModel>> ObterPorId(Guid id)
        {
            var grupo = _mapper.Map<GrupoViewModel>(await _grupoRepository.ObterGrupoPosts(id));
            if (grupo == null)
            {
                NotificarErro("O Grupo não foi encotrado");
                return CustomResponse(grupo);
            }

            return CustomResponse(grupo);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult<GrupoViewModel>> Adicionar(GrupoViewModel grupoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!AppUser.IsAuthenticated())
            {
                NotificarErro("Você precisa estar autenticado");
                return CustomResponse(grupoViewModel);
            }

            var usuario = await _usuarioRepository.ObterPorId(AppUser.GetUserId());

            var grupo = _mapper.Map<Grupo>(grupoViewModel);
            grupo.UsuarioId = usuario.Id;

            await _grupoService.Adicionar(grupo);

            return CustomResponse(grupo);
        }

        [HttpPut("atualizar/{id:guid}")]
        public async Task<ActionResult<GrupoViewModel>> Atualizar(Guid id, GrupoViewModel grupoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            if (!AppUser.IsAuthenticated())
            {
                NotificarErro("Você precisa estar autenticado");
                return CustomResponse(grupoViewModel);
            }

            if (id != grupoViewModel.Id)
            {
                NotificarErro("Houve algum problema ao editar o grupo");
                return CustomResponse(grupoViewModel);
            }

            var usuario = await _usuarioRepository.ObterPorId(AppUser.GetUserId());

            var grupo = _mapper.Map<Grupo>(grupoViewModel);
            grupo.UsuarioId = usuario.Id;

            await _grupoService.Atualizar(grupo);

            return CustomResponse(grupo);
        }

        [HttpDelete("excluir/{id:guid}")]
        public async Task<ActionResult<GrupoViewModel>> Excluir(Guid id)
        {
            var grupo = await _grupoRepository.ObterPorId(id);

            if(grupo == null)
            {
                NotificarErro("Houve um problema ao tentar remover o grupo!");
                return CustomResponse();
            }

            await _grupoRepository.Remover(id);

            return CustomResponse();
        } 
    }

}
