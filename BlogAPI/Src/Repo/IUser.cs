using System.Threading.Tasks;
using BlogAPI.Src.Models;

namespace BlogAPI.Src.Repo
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de
    ///Sser</para>
    /// <para>Criado por: Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>

    public interface IUser
    {
        Task<User> GetUserByEmailAsync(string email);
        Task NewUserAsync (User user); 
    }
}
