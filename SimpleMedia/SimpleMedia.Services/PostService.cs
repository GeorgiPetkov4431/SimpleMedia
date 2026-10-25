using Microsoft.EntityFrameworkCore;
using SimpleMedia.Data;
using SimpleMedia.Data.Models;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Post;
using static SimpleMedia.Common.ApplicationConstants;

namespace SimpleMedia.Services
{
    public class PostService : IPostService
    {
        private readonly SimpleMediaDbContext _dbContext;

        public PostService(SimpleMediaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<PostAllViewModel>> GetAllPostsAsync()
        {
            IEnumerable<PostAllViewModel> allPosts = await this._dbContext.Posts
                .OrderByDescending(p => p.CreatedOn)
                .ThenBy(p => p.Title)
                .ThenBy(p => p.User.UserName)
                .Select(p => new PostAllViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    UserName = p.User.UserName!,
                    CreatedOn = p.CreatedOn.ToString(DateTimeFormat)
                })
                .ToArrayAsync();

            return allPosts;
        }

        public async Task CreatePostAsync(PostFormModel inputModel, string userId)
        {
            Post postToCreate = new Post()
            {
                Title = inputModel.Title,
                Content = inputModel.Content,
                CreatedOn = DateTime.UtcNow,
                UserId = userId
            };

            await this._dbContext.Posts.AddAsync(postToCreate);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<PostDetailsViewModel?> GetPostDetailsAsync(int postId)
        {
            Post? post = await this._dbContext.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return null;
            }

            PostDetailsViewModel postDetails = new PostDetailsViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserName = post.User.UserName!,
                CreatedOn = post.CreatedOn.ToString(DateTimeFormat)
            };

            return postDetails;
        }
    }
}
