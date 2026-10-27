using SimpleMedia.Web.ViewModels.Comment;

namespace SimpleMedia.Services.Contracts
{
    public interface ICommentService
    {
        Task<CommentFormModel> GetCommentFormModelAsync(int postId);

        Task AddCommentAsync(CommentFormModel commentForm, string userId);
    }
}
