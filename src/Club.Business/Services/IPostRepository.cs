using Club.Business.Models;
using System;
using System.Threading.Tasks;

namespace Club.Business.Services
{
    public interface IPostService : IDisposable
    {
        Task<bool> Adicionar(Post post);
        Task<bool> Atualizar(Post post);
        Task<bool> Remover(Guid postId);

        Task<bool> AdicionarFeed(PostFeed post);
        Task<bool> AtualizarFeed(PostFeed post);
        Task<bool> RemoverFeed(Guid postId);
    }
}
