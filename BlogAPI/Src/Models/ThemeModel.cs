using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogAPI.Src.Models
{
    /// <sumary>
    /// <para>Resumo: Classe responsavel por representar tb_themes no banco.</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 02/08/2022</para>
    /// </sumary>
    [Table("tb_themes")]
    public class Theme
    {
        #region Attributes

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        [JsonIgnore, InverseProperty("Theme")]
        public List<Post> MyPosts { get; set; }
        #endregion
    }
}
