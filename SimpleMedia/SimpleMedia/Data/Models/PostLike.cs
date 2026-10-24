using Microsoft.AspNetCore.Identity;

namespace SimpleMedia.Data.Models
{
    public class PostLike
    {
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;

        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
