using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageApp.Data.Models
{
    /// <summary>
    /// We focus only on full matched images
    /// </summary>
    public class ImageModelWebMatchesDb
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
        public string Url { get; set; }
        public string UrlImage { get; set; }
        public string PageTitle { get; set; }
    }
}
