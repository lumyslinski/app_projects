using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImageApp.Data.Contracts;

namespace ImageApp.Data.Models
{
    public class ImageModelDetailsDb: IImageModelDetails
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
        /// <summary>
        /// Opaque entity ID. Some IDs may be available in Google Knowledge Graph Search API.
        /// https://developers.google.com/knowledge-graph/reference/rest/v1/
        /// </summary>
        [Required]
        public string Mid { get; set; }
        /// <summary>
        /// Entity textual description, expressed in its locale language.
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Overall score of the result. Range [0, 1].
        /// </summary>
        [Required]
        public double Score { get; set; }
        public double Topicality { get; set; }
    }
}
