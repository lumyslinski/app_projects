using System;
using System.Collections.Generic;
using System.Text;

namespace ImageApp.Data.Contracts
{
    public interface IImageModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        long Length { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        string ContentImageUrl { get; set; }
        string ContentImageUrlThumb { get; set; }
        string ContentType { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
