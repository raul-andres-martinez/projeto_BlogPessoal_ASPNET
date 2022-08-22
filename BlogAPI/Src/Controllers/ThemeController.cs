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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllThemesAsync()
        {
            var list = await _repo.GetAllThemesAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

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
