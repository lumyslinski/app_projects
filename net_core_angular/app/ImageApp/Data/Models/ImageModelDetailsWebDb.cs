using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageApp.Data.Models
{
    public class ImageModelDetailsWebDb
    {
        #region Lazy-Load Properties
        /// <summary>
        /// The parent Image
        /// </summary>
        [ForeignKey("ImageId")]
        public virtual ImageModelDb Image { get; set; }
        #endregion
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public int ImageId { get; set; }
        public string EntityId { get; set; }
        public double Score { get; set; }
        public string Description { get; set; }
    }
}
