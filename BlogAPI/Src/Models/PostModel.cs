using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Src.Models
{
    /// <sumary>
    /// <para>Resumo: Classe responsavel por representar tb_posts no banco.</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 02/08/2022</para>
    /// </sumary>
    [Table("tb_posts")]
    public class Post
    {

        #region Attributes

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        [ForeignKey("fk_user")]
        public User Author { get; set; }

        [ForeignKey("fk_theme")]
        public Theme Theme { get; set; }
        #endregion
    }
}
