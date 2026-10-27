using SimpleMedia.Web.ViewModels.Tag;
using System.ComponentModel.DataAnnotations;
using static SimpleMedia.Common.EntityValidation.Post;

namespace SimpleMedia.Web.ViewModels.Post
{
    public class PostFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "The Title of your post must be between {2} and {1} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = "The Content of your post must be between {2} and {1} characters long.")]
        public string Content { get; set; } = null!;

        public int TagId { get; set; }

        public IEnumerable<TagDropDownViewModel> Tags
            = new List<TagDropDownViewModel>();
    }
}
