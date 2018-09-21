using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestApp.Data.Models
{
    public class CharacterModelDatabase
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<CharacterEpisodeModelDatabase> Episodes { get; set; }
        public virtual ICollection<CharacterFriendModelDatabase> Friends { get; set; }
    }
}
