using Microsoft.EntityFrameworkCore;
using SimpleMedia.Data;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Tag;

namespace SimpleMedia.Services
{
    public class TagService : ITagService
    {
        private readonly SimpleMediaDbContext _dbContext;

        public TagService(SimpleMediaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<TagDropDownViewModel>> LoadTagsDropDownViewAsync()
        {
            IEnumerable<TagDropDownViewModel> tagsDropDownView = await this._dbContext.Tags
                .OrderBy(t => t.Id)
                .Select(t => new TagDropDownViewModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToArrayAsync();

            return tagsDropDownView;
        }

        public async Task<bool> CheckTagExists(int tagId)
        {
            return await this._dbContext.Tags
                .AnyAsync(t => t.Id == tagId);
        }
    }
}
