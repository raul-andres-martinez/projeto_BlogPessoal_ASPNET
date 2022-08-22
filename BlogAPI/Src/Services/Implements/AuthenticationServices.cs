using BlogAPI.Src.Models;
using BlogAPI.Src.Repo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Src.Services.Implements
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar a autenticação</para>
    /// <para>Criado por: Raul e Generation</para>
    public class AuthenticationServices : IAuthentication
    {
        #region Attributes

        private IUser _repo;
        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        public AuthenticationServices(IUser repo, IConfiguration configuration)
        {
            _repo = repo;
            Configuration = configuration;
        }

        #endregion

        #region Methods

        public string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public async Task CreateNoDuplicateUserAsync (User user)
        {
            var aux = await _repo.GetUserByEmailAsync(user.Email);

            if (aux != null) throw new Exception("Email inválido.");

            user.Password = EncodePassword(user.Password);

            await _repo.NewUserAsync(user);
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Role, user.Type.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
