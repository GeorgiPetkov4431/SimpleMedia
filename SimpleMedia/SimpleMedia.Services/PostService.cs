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

        public IEnumerable<PostAllViewModel> GetAllPosts()
        {
            IEnumerable<PostAllViewModel> allPosts = this._dbContext.Posts
                .Select(p => new PostAllViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    UserName = p.User.UserName!,
                    CreatedOn = p.CreatedOn.ToString(DateTimeFormat)
                })
                .ToArray();

            return allPosts;
        }

        public void CreatePost(PostFormModel inputModel, string userId)
        {
            Post postToCreate = new Post()
            {
                Title = inputModel.Title,
                Content = inputModel.Content,
                CreatedOn = DateTime.UtcNow,
                UserId = userId
            };

            this._dbContext.Posts.Add(postToCreate);
            this._dbContext.SaveChanges();
        }
    }
}
