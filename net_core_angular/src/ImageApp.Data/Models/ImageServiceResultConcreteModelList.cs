using ImageApp.Data.Contracts;
using System.Collections.Generic;

namespace ImageApp.Data.Models
{
    public class ImageServiceResultConcreteModelList : ImageServiceResultBase<IEnumerable<ImageModelDb>>
    {
        public override void SetData(IEnumerable<ImageModelDb> data)
        {
            if (data != null)
                this.SetId(1);
            base.SetData(data);
        }
    }
}
