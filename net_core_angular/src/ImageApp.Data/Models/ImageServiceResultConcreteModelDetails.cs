using ImageApp.Data.Contracts;

namespace ImageApp.Data.Models
{
    public class ImageServiceResultConcreteModelDetails : ImageServiceResultBase<ImageModelDetailsDb>
    {
        public override void SetData(ImageModelDetailsDb data)
        {
            if (data != null)
                this.SetId(data.Id);
            base.SetData(data);
        }
    }
}
