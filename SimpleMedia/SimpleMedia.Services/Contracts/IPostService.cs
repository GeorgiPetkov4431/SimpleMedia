using SimpleMedia.Web.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMedia.Services.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostAllViewModel>> GetAllPostsAsync(string? keyword, int? tagId);

        Task<IEnumerable<PostAllViewModel>?> GetTopPostsAsync();

        Task<PostFormModel> GetPostFormModelAsync();

        Task CreatePostAsync(PostFormModel inputModel, string userId);

        Task<PostDetailsViewModel?> GetPostDetailsAsync(int postId, string? userId);

        Task<PostFormModel?> GetPostForEditingAsync(int postId, string userId);

        Task EditPostAsync(int postId, PostFormModel model);

        Task<PostDeleteViewModel?> GetPostForDeletion(int postId, string userId);

        Task DeletePostAsync(int postId);
    }
}
