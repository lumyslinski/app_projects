using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApp.Data.Models
{
    public class EpisodeModelDatabase
    {
        #region Lazy-Load Properties
        public virtual CharacterModelDatabase Character { get; set; }
        #endregion
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
