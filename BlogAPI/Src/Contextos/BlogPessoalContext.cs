using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Src.Contextos
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsável por carregar o contexto e definir DbSets</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data 03/08/2022</para>
    /// </summary>
    public class BlogPessoalContext : DbContext
    {
        #region Attributes

        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Post> Posts { get; set; }

        #endregion

        #region Constructors

        public BlogPessoalContext(DbContextOptions<BlogPessoalContext> opt) : base(opt)
        {

        }

        #endregion
    }
}
