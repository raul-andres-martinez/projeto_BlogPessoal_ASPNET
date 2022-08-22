using BlogAPI.Src.Models;
using BlogAPI.Src.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controllers
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar o ThemeController</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 08/08/2022</para>
    /// </summary>
    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Attributes

        private readonly ITheme _repo;

        #endregion

        #region Constructors

        public ThemeController(ITheme repo)
        {
            _repo = repo;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Pegar todos os temas
        /// </summary>
        /// <param>Pegar todos os temas</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna todos temas</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllThemesAsync()
        {
            var list = await _repo.GetAllThemesAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        ///<summary>
        /// Pegar tema por id
        /// </summary>
        /// <param name="id_theme">Pegar tema por id</param>
        /// <remarks>
        /// api/Temas/{ID}
        /// </remarks>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna tema</response>
        /// <response code="404">Tema não existente</response>
        [HttpGet("id/{id_theme}")]
        [Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int id_theme)
        {
            try
            {
                return Ok(await _repo.GetThemeByIdAsync(id_theme));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Criar novo tema
        /// </summary>
        /// <param name="theme">Contrutor para criar tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Temas
        /// {
        /// "description": "uma linguagem de programação",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Erro de entrada</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewThemeAsync([FromBody] Theme theme)
        {
            try
            {
                await _repo.NewThemeAsync(theme);
                return Created($"api/Temas", theme);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        /// <summary>
        /// Atualizar tema
        /// </summary>
        /// <param name="theme">Contrutor para atualizar tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Temas
        /// {
        /// "description": "uma linguagem de programação",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Erro de entrada</response>
        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> UpdateThemeAsync([FromBody] Theme theme)
            {
                try
                {
                    await _repo.UpdateThemeAsync(theme);
                    return Ok(theme);
                }
                catch(Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }

        ///<summary>
        /// Deletar tema por id
        /// </summary>
        /// <param name="id_theme">Deletar tema por id</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Tema excluido</response>
        /// <response code="404">Tema não existente</response>
        [HttpDelete("id/{id_theme}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeleteThemeAsync([FromRoute] int id_theme)
        {
            try
            {
                await _repo.DeleteThemeAsync(id_theme);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }

        }
            #endregion

    }
}
