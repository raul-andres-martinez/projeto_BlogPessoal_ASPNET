using BlogAPI.Src.Models;
using BlogAPI.Src.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controllers

{
    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Attributes

        private readonly IPost _repo;

        #endregion

        #region Constructors

        public PostController(IPost repo)
        {
            _repo = repo;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Pegar todos posts
        /// </summary>
        /// <param>Pegar todos os posts</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna todos posts</response>

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repo.GetAllPostsAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }
        ///<summary>
        /// Pegar post por id
        /// </summary>
        /// <param name="id_post">Pegar post por id</param>
        /// <remarks>
        /// GET /api/Postagens/{ID}
        /// </remarks>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna post</response>
        /// <response code="404">Post não existente</response>
        [HttpGet("id/{id_post}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int id_post)
        {
            try
            {
                return Ok(await _repo.GetPostByIdAsync(id_post));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Criar novo post
        /// </summary>
        /// <param name="post">Contrutor para criar post</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Postagens
        /// {
        /// "title": "C#",
        /// "description": "uma linguagem de programação",
        /// "photo": "URLFOTO",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna post criado</response>
        /// <response code="400">Erro de entrada</response>


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewPostAsync([FromBody] Post post)
        {
            try
            {
                await _repo.NewPostAsync(post);
                return Created($"api/Postagens", post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Atualizar post
        /// </summary>
        /// <param name="post">Contrutor para atualizar post</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Postagens
        /// {
        /// "title": "C#",
        /// "description": "uma linguagem de programação",
        /// "photo": "URLFOTO",
        /// }
        ///
        /// </remarks>
        /// <response code="200">Retorna post atualizado</response>
        /// <response code="400">Erro de entrada</response>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdatePostAsync([FromBody] Post post)
        {
            try
            {
                await _repo.UpdatePostAsync(post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Deletar post por id
        /// </summary>
        /// <param name="id_post">Deletar post por id</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna post deletado</response>
        /// <response code="404">Post não existente</response>
        [HttpDelete("deletar/{id_post}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int id_post)
        {
            try
            {
                await _repo.DeletePostAsync(id_post);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            #endregion

        }
    }
}

