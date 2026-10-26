using Microsoft.EntityFrameworkCore;
using SimpleMedia.Data;
using SimpleMedia.Data.Models;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Post;
using System.Formats.Asn1;
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

        public async Task<IEnumerable<PostAllViewModel>> GetAllPostsAsync(string? keyword)
        {
            IQueryable<Post> query = this._dbContext.Posts
                .OrderByDescending(p => p.CreatedOn)
                .ThenBy(p => p.Title)
                .ThenBy(p => p.User.UserName);

            if (keyword != null)
            {
                query = query
                    .Where(e => EF.Functions.Like(e.Title, $"%{keyword}%"));
            }

            IEnumerable<PostAllViewModel> allPosts = await query
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

        public async Task<PostFormModel?> GetPostForEditingAsync(int postId, string userId)
        {
            Post? post = await this._dbContext.Posts
                .FindAsync(postId);

            if (post == null)
            {
                return null;
            }

            if (post.UserId != userId)
            {
                throw new UnauthorizedAccessException("You cannot edit a post that isn't your own!");
            }

            PostFormModel postToEditForm = new PostFormModel()
            {
                Title = post.Title,
                Content = post.Content
            };

            return postToEditForm;
        }

        public async Task EditPostAsync(int postId, PostFormModel model)
        {
            Post? post = await this._dbContext.Posts
                .FindAsync(postId);

            if (post == null)
            {
                throw new InvalidOperationException("Post not found.");
            }

            post.Title = model.Title;
            post.Content = model.Content;

            await this._dbContext.SaveChangesAsync();
        }

        public async Task<PostDeleteViewModel?> GetPostForDeletion(int postId, string userId)
        {
            Post? post = await this._dbContext.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return null;
            }

            if (post.UserId != userId)
            {
                throw new UnauthorizedAccessException("You cannot delete a post that isn't your own!");
            }

            PostDeleteViewModel postToDelete = new PostDeleteViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                UserName = post.User.UserName!,
                CreatedOn = post.CreatedOn.ToString(DateTimeFormat)
            };

            return postToDelete;
        }

        public async Task DeletePostAsync(int id)
        {
            Post? post = await this._dbContext.Posts
                .FindAsync(id);

            if (post == null)
            {
                throw new InvalidOperationException("Post not found!");
            }

            this._dbContext.Posts.Remove(post);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
