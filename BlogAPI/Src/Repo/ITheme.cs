using BlogAPI.Src.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repo
{
    /// <sumary>
    /// <para>Resumo: Responsavel por representar ações de CRUD do Theme.</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 08/08/2022</para>
    /// </sumary>
    public interface ITheme
    {
        Task<List<Theme>> GetAllThemesAsync();
        Task<Theme> GetThemeByIdAsync(int id);
        Task NewThemeAsync(Theme theme);
        Task UpdateThemeAsync(Theme theme);
        Task DeleteThemeAsync(int id);
    }
}
