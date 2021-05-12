using Club.Business.Interfaces;
using Club.Business.Models;
using Club.Business.Models.Validation;
using System;
using System.Threading.Tasks;

namespace Club.Business.Services
{
    public class PostService : BaseService, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostFeedRepository _postFeedRepository;

        public PostService(IPostRepository postRepository, IPostFeedRepository postFeedRepository, INotificador notificador) : base (notificador)
        {
            _postRepository = postRepository;
            _postFeedRepository = postFeedRepository;
        }

        public async Task<bool> Adicionar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return false;

            await _postRepository.Adicionar(post);
            return true;
        }

        public async Task<bool> Atualizar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return false;
            await _postRepository.Atualizar(post);
            return true;
        }

        public async Task<bool> Remover(Guid postId)
        {
            await _postRepository.Remover(postId);
            return true;
        }

        public async Task<bool> AdicionarFeed(PostFeed post)
        {
            if (!ExecutarValidacao(new PostFeedValidation(), post)) return false;

            await _postFeedRepository.Adicionar(post);
            return true;
        }

        public async Task<bool> AtualizarFeed(PostFeed post)
        {
            if (!ExecutarValidacao(new PostFeedValidation(), post)) return false;
            await _postFeedRepository.Atualizar(post);
            return true;
        }

        public async Task<bool> RemoverFeed(Guid postId)
        {
            await _postFeedRepository.Remover(postId);
            return true;
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }
    }
}
