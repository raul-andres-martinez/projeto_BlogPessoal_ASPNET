using BlogAPI.Src.Models;
using BlogAPI.Src.Repo;
using BlogAPI.Src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes

        private readonly IUser _repo;
        private readonly IAuthentication services;

        #endregion

        #region Constructors

        public UserController(IUser repo, IAuthentication services)
        {
            _repo = repo;
            this.services = services;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Pegar usuario pelo Email
        /// </summary>
        /// <param name="email_user">E-mail do usuario</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Email não existente</response>

        [HttpGet("email/{email_user}")]
        [Authorize(Roles = "NORMAL,ADMINISTRADOR")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string email_user)
        {
            var user = await _repo.GetUserByEmailAsync(email_user);

            if (user == null) return NotFound(new { Message = "Usuário não encontrado" });

            return Ok(User);
        }
        /// <summary>
        /// Criar novo Usuario
        /// </summary>
        /// <param name="user">Contrutor para criar usuario</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Usuarios/cadastrar
        /// {
        /// "name": "Fulano",
        /// "email": "fulano@email.com",
        /// "password": "134652",
        /// "photo": "URLFOTO",
        /// "type": "NORMAL"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [HttpPost("cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult> NewUserAsync([FromBody] User user)
        {
            try
            {
                await services.CreateNoDuplicateUserAsync(user);

                return Created($"api/Usuarios/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        /// <summary>
        /// Pegar Autorização
        /// </summary>
        /// <param name="user">Construtor para logar usuario</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Usuarios/logar
        /// {
        /// "email": "fulano@email.com",
        /// "password": "134652"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="401">E-mail ou senha invalido</response>
        [HttpPost("logar")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] User user)
        {
            var aux = await _repo.GetUserByEmailAsync(user.Email);

            if (aux == null) return Unauthorized(new { Message = "Dados inválidos" });

            if (aux.Password != services.EncodePassword(user.Password))
                return Unauthorized(new { Message = "Dados inválidos" });

            var token = "Bearer " + services.GenerateToken(aux);

            return Ok(new { User = aux, Token = token });
        }
        #endregion
    }
}
