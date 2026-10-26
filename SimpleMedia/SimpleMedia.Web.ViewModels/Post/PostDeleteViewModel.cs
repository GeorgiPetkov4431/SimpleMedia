using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMedia.Web.ViewModels.Post
{
    public class PostDeleteViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;
    }
}
