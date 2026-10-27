using Microsoft.AspNetCore.Mvc;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Like;

namespace SimpleMedia.Controllers
{
    public class LikeController : BaseController
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            this._likeService = likeService;
        }

        public async Task<IActionResult> All()
        {
            string userId = ValidateAuthorizedUserId(GetUserId());
            IEnumerable<LikeAllViewModel> likedPosts = await this._likeService
                .GetAllLikedPostsAsync(userId);

            return View(likedPosts);
        }

        public async Task<IActionResult> Unlike(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            string userId = ValidateAuthorizedUserId(GetUserId());
            try
            {
                await this._likeService.LikePostAsync(id.Value, userId);

                return RedirectToAction(nameof(All));
            }
            catch (InvalidOperationException e)
            {
                TempData["Error"] = e.Message;

                return RedirectToAction("All", "Post");
            }
        }
    }
}
