using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogAPI.Src.Models
{
    /// <sumary>
    /// <para>Resumo: Classe responsavel por representar tb_users no banco.</para>
    /// <para>Criado por: Raul e Generation</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 02/08/2022</para>
    /// </sumary>
    [Table("tb_users")]
    public class User
    {
        #region Attributes

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Photo { get; set; }

        [JsonIgnore, InverseProperty("Author")]
        public List<Post> MyPosts { get; set; }
        #endregion
    }
}
