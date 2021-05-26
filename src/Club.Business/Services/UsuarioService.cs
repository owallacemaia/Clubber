using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Business.Models.Validation;
using System;
using System.Threading.Tasks;

namespace Club.Business.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository
            , INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Adicionar(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return false;

            await _usuarioRepository.Adicionar(usuario);
            return true;
        }

        public async Task<bool> Atualizar(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return false;

            await _usuarioRepository.Atualizar(usuario);
            return true;
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();
        }
    }
}
