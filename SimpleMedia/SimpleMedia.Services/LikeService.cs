using Microsoft.EntityFrameworkCore;
using SimpleMedia.Data;
using SimpleMedia.Data.Models;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Like;

using static SimpleMedia.Common.ApplicationConstants;

namespace SimpleMedia.Services
{
    public class LikeService : ILikeService
    {
        private readonly SimpleMediaDbContext _dbContext;

        public LikeService(SimpleMediaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LikeAllViewModel>> GetAllLikedPostsAsync(string userId)
        {
            IEnumerable<LikeAllViewModel> likedPosts = await this._dbContext.PostLikes
                .OrderByDescending(pl => pl.LikedOn)
                .Where(pl => pl.UserId == userId)
                .Select(pl => new LikeAllViewModel()
                {
                    PostId = pl.Post.Id,
                    Title = pl.Post.Title,
                    UserName = pl.User.UserName!,
                    CreatedOn = pl.Post.CreatedOn.ToString(DateTimeFormat)
                })
                .ToArrayAsync();

            return likedPosts;
        }

        public async Task LikePostAsync(int postId, string userId)
        {
            bool postExists = await this._dbContext.Posts
                .AnyAsync(p => p.Id == postId);

            if (!postExists)
            {
                throw new InvalidOperationException("Post not found!");
            }

            PostLike? alreadyLiked = await this._dbContext.PostLikes
                .FirstOrDefaultAsync(pl => pl.UserId == userId && pl.PostId == postId);

            if (alreadyLiked == null)
            {
                PostLike postLike = new PostLike()
                {
                    UserId = userId,
                    PostId = postId,
                    LikedOn = DateTime.UtcNow
                };

                await this._dbContext.PostLikes.AddAsync(postLike);
            }
            else
            {
                this._dbContext.PostLikes.Remove(alreadyLiked);
            }

            await this._dbContext.SaveChangesAsync();
        }
    }
}
