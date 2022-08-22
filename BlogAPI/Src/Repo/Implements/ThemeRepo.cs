using BlogAPI.Src.Contextos;
using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repo.Launch
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar ITheme</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 05/08/2022</para>
    /// </summary>

    public class ThemeRepo : ITheme
    {
        #region Attributes

        private readonly BlogPessoalContext _context;

        #endregion Attributes

        #region Constructors

        public ThemeRepo(BlogPessoalContext context)
        {
            _context = context;
        }
        #endregion Constructors
        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todos temas</para>
        /// </summary>
        /// <return>Lista TemaModelo</return>
        #region Methods
        public async Task<List<Theme>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar um tema pelo Id</para>
        /// </summary>
        /// <param name="id">Id do tema</param>
        /// <return>TemaModelo</return>
        /// <exception cref="Exception">Id não pode ser nulo</exception>
        public async Task<Theme> GetThemeByIdAsync(int id)
        {
            if (!IdExist(id)) throw new Exception("Id do tema não encontrado");
            return await _context.Themes.FirstOrDefaultAsync(t => t.Id == id);

            //função auxiliar
            bool IdExist(int id)
            {
                var aux = _context.Themes.FirstOrDefault(t => t.Id == id);
                return aux != null;
            }
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo tema</para>
        /// </summary>
        /// <param name="theme">Construtor para cadastrar tema</param>
        public async Task NewThemeAsync(Theme theme)
        {
            if (await DescriptionExist(theme.Description)) throw new Exception("Descrição ja existente.");

            await _context.Themes.AddAsync(
                new Theme
                {
                    Description = theme.Description
                });
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um tema</para>
        /// </summary>
        /// <param name="theme">Construtor para atualizar tema</param>
        public async Task UpdateThemeAsync(Theme theme)
        {
            if (!DescriptionExist(theme.Description)) throw new Exception("Descrição ja existente.");

            var themeExist = await GetThemeByIdAsync(theme.Id);
            themeExist.Description = theme.Description;
            _context.Themes.Update(themeExist);
            await _context.SaveChangesAsync();

            bool DescriptionExist(string description)
            {
                var aux = _context.Themes.FirstOrDefault(t => t.Description == description);

                return aux != null;
            }
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um tema</para>
        /// </summary>
        /// <param name="id">Id do tema</param>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();  
        }

        private async Task<bool> DescriptionExist(string description)
        {
            var aux = await _context.Themes.FirstOrDefaultAsync(t => t.Description == description);
            return aux != null;
        }
        #endregion Methods
    }
}
