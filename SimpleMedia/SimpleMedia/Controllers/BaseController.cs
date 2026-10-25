using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SimpleMedia.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string ValidateAuthorizedUserId(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidOperationException("General identity error occurred - id is null in authorized area!");
            }

            return userId;
        }

        protected string? GetUserId()
        {
            string? userId = null;
            Claim? idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim != null)
            {
                userId = idClaim.Value;
            }

            return userId;
        }
    }
}
