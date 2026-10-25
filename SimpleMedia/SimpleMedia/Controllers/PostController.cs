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
        public async Task<IActionResult> Details([FromRoute] int? id)
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            try
            {
                PostFormModel? postForEditing = await this._postService
                .GetPostForEditingAsync(id.Value, userId);

                if (postForEditing == null)
                {
                    return NotFound();
                }

                return View(postForEditing);
            }
            catch (UnauthorizedAccessException e)
            {
                TempData["Error"] = e.Message;

                return RedirectToAction(nameof(All));
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int? id, PostFormModel model)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "There was an error with your submission.");

                return View(model);
            }

            try
            {
                await this._postService.EditPostAsync(id.Value, model);
            }
            catch (InvalidOperationException e)
            {
                TempData["Error"] = e.Message;

                return RedirectToAction(nameof(All));
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return View();
        }
    }
}
