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
        public async Task<IActionResult> All()
        {
            IEnumerable<PostAllViewModel> allposts = await this._postService
                .GetAllPostsAsync();

            return View(allposts);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "There was an error with your submission.");
                return View(model);
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            await this._postService.CreatePostAsync(model, userId);

            //ViewBag.SuccessMessage = "Post created successfully!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            PostDetailsViewModel? postDetails = await this._postService
                .GetPostDetailsAsync(id.Value);

            if (postDetails == null)
            {
                return NotFound();
            }

            return View(postDetails);
        }
    }
}
