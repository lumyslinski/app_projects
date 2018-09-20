using System.ComponentModel.DataAnnotations;

namespace RestApp.Data.Models
{
    public class FriendModelDatabase
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
