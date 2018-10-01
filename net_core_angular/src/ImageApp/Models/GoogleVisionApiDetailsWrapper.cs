using System;
using System.Collections.Generic;
using ImageApp.Data.Models;
using Newtonsoft.Json;

namespace ImageApp.Models
{
    public class GoogleVisionApiDetailsWrapper
    {
        private GoogleVisionApiDetailsDto[] googleVisionApiResults = null;
        private int elementCurrent = 0;
        private int elementMax = 3;

        public GoogleVisionApiDetailsWrapper(int elementMax = 3)
        {
            this.elementMax = elementMax;
            googleVisionApiResults = new GoogleVisionApiDetailsDto[this.elementMax];
        }

        public bool Add(ImageModelDetailsDb imageImageModelDetail)
        {
            if (elementCurrent < elementMax)
            {
                googleVisionApiResults[elementCurrent] = new GoogleVisionApiDetailsDto()
                {
                    Description = imageImageModelDetail.Description,
                    Percent = (int)Math.Round(imageImageModelDetail.Score * 100)
                };
                elementCurrent++;
                return true;
            }
            return false;
        }

        public void Add(List<ImageModelDetailsDb> imageImageModelDetails)
        {
            if (imageImageModelDetails != null && imageImageModelDetails.Count > 0)
            {
                foreach (var imageImageModelDetail in imageImageModelDetails)
                {
                    if (!Add(imageImageModelDetail))
                        break;
                }
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(googleVisionApiResults);
        }
    }
}
