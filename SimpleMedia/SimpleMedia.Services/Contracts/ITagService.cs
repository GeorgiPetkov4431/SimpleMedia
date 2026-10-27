using SimpleMedia.Web.ViewModels.Tag;

namespace SimpleMedia.Services.Contracts
{
    public interface ITagService
    {
        Task<IEnumerable<TagDropDownViewModel>> LoadTagsDropDownViewAsync();

        Task<bool> CheckTagExists(int tagId);
    }
}
