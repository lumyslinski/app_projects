using System.Collections.Generic;

namespace ImageApp.Data.Models
{
    public class ImageJsonResultLabelAnnotation
    {
        public string mid { get; set; }
        public string description { get; set; }
        public double score { get; set; }
        public double topicality { get; set; }
    }

    public class ImageJsonResultSafeSearchAnnotation
    {
        public string adult { get; set; }
        public string spoof { get; set; }
        public string medical { get; set; }
        public string violence { get; set; }
        public string racy { get; set; }
    }

    public class ImageJsonResultColor2
    {
        public int red { get; set; }
        public int green { get; set; }
        public int blue { get; set; }
    }

    public class ImageJsonResultColor
    {
        public ImageJsonResultColor2 color { get; set; }
        public double score { get; set; }
        public double pixelFraction { get; set; }
    }

    public class ImageJsonResultDominantColors
    {
        public List<ImageJsonResultColor> colors { get; set; }
    }

    public class ImageJsonResultPropertiesAnnotation
    {
        public ImageJsonResultDominantColors ImageJsonResultDominantColors { get; set; }
    }

    public class ImageJsonResultVertex
    {
        public int? x { get; set; }
        public int? y { get; set; }
    }

    public class ImageJsonResultBoundingPoly
    {
        public List<ImageJsonResultVertex> vertices { get; set; }
    }

    public class ImageJsonResultCropHint
    {
        public ImageJsonResultBoundingPoly ImageJsonResultBoundingPoly { get; set; }
        public double confidence { get; set; }
        public int importanceFraction { get; set; }
    }

    public class ImageJsonResultCropHintsAnnotation
    {
        public List<ImageJsonResultCropHint> cropHints { get; set; }
    }

    public class ImageJsonResultWebEntity
    {
        public string entityId { get; set; }
        public double score { get; set; }
        public string description { get; set; }
    }

    public class ImageJsonResultFullMatchingImage
    {
        public string url { get; set; }
    }

    public class ImageJsonResultPartialMatchingImage
    {
        public string url { get; set; }
    }

    public class ImageJsonResultFullMatchingImage2
    {
        public string url { get; set; }
    }

    public class ImageJsonResultPartialMatchingImage2
    {
        public string url { get; set; }
    }

    public class ImageJsonResultPagesWithMatchingImage
    {
        public string url { get; set; }
        public string pageTitle { get; set; }
        public List<ImageJsonResultFullMatchingImage2> fullMatchingImages { get; set; }
        public List<ImageJsonResultPartialMatchingImage2> partialMatchingImages { get; set; }
    }

    public class ImageJsonResultBestGuessLabel
    {
        public string label { get; set; }
        public string languageCode { get; set; }
    }

    public class ImageJsonResultWebDetection
    {
        public List<ImageJsonResultWebEntity> webEntities { get; set; }
        public List<ImageJsonResultFullMatchingImage> fullMatchingImages { get; set; }
        public List<ImageJsonResultPartialMatchingImage> partialMatchingImages { get; set; }
        public List<ImageJsonResultPagesWithMatchingImage> pagesWithMatchingImages { get; set; }
        public List<ImageJsonResultBestGuessLabel> bestGuessLabels { get; set; }
    }

    public class ImageJsonResult
    {
        public List<ImageJsonResultLabelAnnotation> labelAnnotations { get; set; }
        public ImageJsonResultSafeSearchAnnotation ImageJsonResultSafeSearchAnnotation { get; set; }
        public ImageJsonResultPropertiesAnnotation ImageJsonResultPropertiesAnnotation { get; set; }
        public ImageJsonResultCropHintsAnnotation ImageJsonResultCropHintsAnnotation { get; set; }
        public ImageJsonResultWebDetection ImageJsonResultWebDetection { get; set; }
    }

    public class GoogleVisionApiJsonResponse
    {
        public List<ImageJsonResult> responses { get; set; }
    }
}
