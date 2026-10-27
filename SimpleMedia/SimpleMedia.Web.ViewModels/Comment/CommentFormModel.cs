using System.ComponentModel.DataAnnotations;
using static SimpleMedia.Common.EntityValidation.Comment;

namespace SimpleMedia.Web.ViewModels.Comment
{
    public class CommentFormModel
    {
        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = "Your Comment must be between {2} and {1} characters.")]
        public string Content { get; set; } = null!;

        public int PostId { get; set; }

        public string? PostAuthorUserName { get; set; }
    }
}
