using System.ComponentModel.DataAnnotations;
using static SimpleMedia.Common.EntityValidation.Tag;

namespace SimpleMedia.Data.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public int Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
            = new HashSet<Post>();
    }
}
