using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMedia.Data;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Post;

namespace SimpleMedia.Controllers
{
    public class PostController : BaseController
    {
        private readonly SimpleMediaDbContext _dbContext;

        private readonly IPostService _postService;

        public PostController(SimpleMediaDbContext dbContext, IPostService postService)
        {
            this._dbContext = dbContext;
            this._postService = postService;
        }

        [HttpGet]
        public IActionResult All()
        {
            IEnumerable<PostAllViewModel> allposts = this._postService
                .GetAllPosts();

            return View(allposts);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "There was an error with your submission.");
                return View(model);
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            this._postService.CreatePost(model, userId);

            //ViewBag.SuccessMessage = "Post created successfully!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            PostDetailsViewModel? postDetails = this._postService
                .GetPostDetails(id.Value);

            if (postDetails == null)
            {
                return NotFound();
            }

            return View(postDetails);
        }
    }
}
