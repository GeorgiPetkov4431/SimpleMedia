using Microsoft.AspNetCore.Mvc;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Comment;

namespace SimpleMedia.Controllers
{
    
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        [HttpGet("Post/{postId?}/Comment/Add")]
        public async Task<IActionResult> Add([FromRoute] int? postId)
        {
            if (postId == null || postId <= 0)
            {
                return NotFound();
            }

            try
            {
                CommentFormModel commentFormModel = await this._commentService
                    .GetCommentFormModelAsync(postId.Value);

                return View(commentFormModel);
            }
            catch (InvalidOperationException e)
            {
                TempData["Error"] = e.Message;

                return RedirectToAction("All", "Post");
            }
        }

        [HttpPost("Post/{postId?}/Comment/Add")]
        public async Task<IActionResult> Add(CommentFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            try
            {
                string userId = ValidateAuthorizedUserId(GetUserId());
                await this._commentService.AddCommentAsync(formModel, userId);

                return RedirectToAction("Details", "Post", new { id = formModel.PostId });
            }
            catch
            {
                return RedirectToAction("All", "Post");
            }
        }
    }
}
