using SimpleMedia.Web.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMedia.Services.Contracts
{
    public interface IPostService
    {
        void CreatePost(PostFormModel inputModel, string userId);
    }
}
