using ImageApp.Data.Contracts;

namespace ImageApp.Data.Models
{
    public class ImageServiceResultConcreteModelDetailsWeb : ImageServiceResultBase<ImageModelDetailsWebDb>
    {
        public override void SetData(ImageModelDetailsWebDb data)
        {
            if (data != null)
                this.SetId(data.Id);
            base.SetData(data);
        }
    }
}
