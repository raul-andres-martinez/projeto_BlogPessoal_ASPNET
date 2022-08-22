using BlogAPI.Src.Contextos;
using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repo.Launch
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IUser</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// </summary>
    public class UserRepo : IUser
    {
        #region Attributes

        private readonly BlogPessoalContext _context;

        #endregion

        #region Constructors

        public UserRepo(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um usario pelo
        ///email</para>
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <return>UserModel</return>

        public async Task<User> GetUserByEmailAsync (string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo usuario</para>
        /// </summary>
        /// <param name="user">Construtor para cadastrar usuario</param>

        public async Task NewUserAsync (User user)
        {
            await _context.Users.AddAsync(
                new User
                {
                    Email = user.Email,
                    Name = user.Name,
                    Password = user.Password,
                    Photo = user.Photo,
                    Type = user.Type,
                });
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
