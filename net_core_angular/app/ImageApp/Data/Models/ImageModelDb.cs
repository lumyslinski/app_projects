using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageApp.Data.Models
{
    public class ImageModelDb
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public long Length { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public string ContentImageUrl { get; set; }

        [Required]
        public string ContentImageUrlThumb { get; set; }

        [Required]
        public string ContentType { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }

        #region Lazy-Load Properties
        /// <summary>
        /// ImageModelDetails
        /// </summary>
        public virtual List<ImageModelDetailsDb> ImageModelDetails { get; set; }

        public virtual List<ImageModelDetailsWebDb> ImageModelDetailsWeb { get; set; }

        public virtual List<ImageModelWebMatchesDb> ImageModelWebMatches { get; set; }
        #endregion
    }
}
