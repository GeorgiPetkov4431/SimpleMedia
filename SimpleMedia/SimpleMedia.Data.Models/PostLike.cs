using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SimpleMedia.Common.EntityValidation.PostLike;

namespace SimpleMedia.Data.Models
{
    public class PostLike
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;

        [Column(TypeName = LikedOnColumnType)]
        public DateTime LikedOn { get; set; }
    }
}
