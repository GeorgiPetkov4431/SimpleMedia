using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMedia.Data;
using SimpleMedia.Services.Contracts;
using SimpleMedia.Web.ViewModels.Post;
using System.Security.Claims;

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
    }
}
