using ImageApp.Data.Contracts;

namespace ImageApp.Data.Models
{
    public class ImageServiceResultConcreteModelWebMatches : ImageServiceResultBase<ImageModelWebMatchesDb>
    {
        public override void SetData(ImageModelWebMatchesDb data)
        {
            if (data != null)
                this.SetId(data.Id);
            base.SetData(data);
        }
    }
}
