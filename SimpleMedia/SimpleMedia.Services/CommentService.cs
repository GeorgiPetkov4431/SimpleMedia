using Microsoft.EntityFrameworkCore;
using SimpleMedia.Data;
using SimpleMedia.Data.Models;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Comment;

namespace SimpleMedia.Services
{
    public class CommentService : ICommentService
    {
        private readonly SimpleMediaDbContext _dbContext;

        public CommentService(SimpleMediaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<CommentFormModel> GetCommentFormModelAsync(int postId)
        {
            Post? postExists = await this._dbContext.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == postId);
            if (postExists == null)
            {
                throw new InvalidOperationException("This post does not exist!");
            }

            CommentFormModel commentFormModel = new CommentFormModel()
            {
                PostId = postExists.Id,
                PostAuthorUserName = postExists.User.UserName!
            };

            return commentFormModel;
        }

        public async Task AddCommentAsync(CommentFormModel commentForm, string userId)
        {
            Comment comment = new Comment()
            {
                Content = commentForm.Content,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                PostId = commentForm.PostId
            };

            await this._dbContext.Comments.AddAsync(comment);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
