using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Post;

namespace SimpleMedia.Controllers
{
    public class PostController : BaseController
    {
        private readonly ILogger<PostController> _logger;

        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            this._logger = logger;
            this._postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(string? keyword)
        {
            IEnumerable<PostAllViewModel> allposts = await this._postService
                .GetAllPostsAsync(keyword);

            return View(allposts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

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

            TempData["Success"] = "Post created successfully!";
            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
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

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            try
            {
                PostDeleteViewModel? postForDeletion = await this._postService
                .GetPostForDeletion(id.Value, userId);

                if (postForDeletion == null)
                {
                    return NotFound();
                }

                return View(postForDeletion);
            }
            catch (UnauthorizedAccessException e)
            {
                TempData["Error"] = e.Message;

                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, PostDeleteViewModel model)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            if (id.Value != model.Id)
            {
                return BadRequest("There was an error with your request, try again!");
            }
            
            try
            {
                await this._postService.DeletePostAsync(id.Value);

                TempData["Success"] = "Post deleted successfully!";
                return RedirectToAction(nameof(All));
            }
            catch (InvalidOperationException e)
            {
                this._logger.LogCritical(e, "An error occurred while deleting the post with ID {PostId}.", id.Value);
                TempData["Error"] = e.Message;

                return RedirectToAction(nameof(All));
            }
        }
    }
}
