using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
