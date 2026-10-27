using SimpleMedia.Web.ViewModels.Like;

namespace SimpleMedia.Services.Contracts
{
    public interface ILikeService
    {
        Task<IEnumerable<LikeAllViewModel>> GetAllLikedPostsAsync(string userId);

        Task LikePostAsync(int postId, string userId);
    }
}
