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
        public virtual List<EpisodeModelDatabase> Episodes { get; set; }
        public virtual List<FriendModelDatabase> Friends { get; set; }
    }
}
