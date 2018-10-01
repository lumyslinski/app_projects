using System;
using System.Collections.Generic;

namespace ImageApp.Models
{
    public class ImageJsonRequestContent
    {
        public string content { get; set; }

        public ImageJsonRequestContent(string imageContent)
        {
            this.content = imageContent;
        }
    }

    public class ImageJsonRequestFeature
    {
        public string type { get; set; }
        public int maxResults { get; set; }
    }

    public class ImageJsonRequest
    {
        public ImageJsonRequestContent image { get; set; }
        public List<ImageJsonRequestFeature> features { get; set; }

        public ImageJsonRequest(string imageContent, string featureType=null)
        {
            features = new List<ImageJsonRequestFeature>();
            if (String.IsNullOrEmpty(featureType))
            {
                features.Add(new ImageJsonRequestFeature() { type = "TYPE_UNSPECIFIED", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "LANDMARK_DETECTION", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "FACE_DETECTION", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "LOGO_DETECTION", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "LABEL_DETECTION", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "DOCUMENT_TEXT_DETECTION", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "SAFE_SEARCH_DETECTION", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "IMAGE_PROPERTIES", maxResults = 50 });
                features.Add(new ImageJsonRequestFeature() { type = "WEB_DETECTION", maxResults = 50 });
            }

            image = new ImageJsonRequestContent(imageContent);
        }
    }

    public class GoogleVisionApiJsonRequest
    {
        public List<ImageJsonRequest> requests { get; set; }

        public GoogleVisionApiJsonRequest()
        {
            requests = new List<ImageJsonRequest>();
        }
    }
}
