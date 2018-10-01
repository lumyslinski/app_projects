using ImageApp.Data.Contracts;

namespace ImageApp.Data.Models
{
    public class ImageServiceResultConcreteModel: ImageServiceResultBase<ImageModelDb>
    {
        public override void SetData(ImageModelDb data)
        {
            if (data != null)
                this.SetId(data.Id);
            base.SetData(data);
        }
    }
}
