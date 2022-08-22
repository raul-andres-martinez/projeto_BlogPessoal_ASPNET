using BlogAPI.Src.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repo
{
    /// <sumary>
    /// <para>Resumo: Responsavel por representar ações de CRUD do Post.</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 08/08/2022</para>
    /// </sumary>
    public interface IPost
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task NewPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
    }
}
