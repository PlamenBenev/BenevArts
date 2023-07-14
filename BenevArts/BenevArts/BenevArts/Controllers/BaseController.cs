using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BenevArts.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string GetUserId()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            //if (userId != null)
            //{
            //    userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //}

            return userId!;
        }
        protected string GetUsername()
        {
            string username = string.Empty;

            if (username != null)
            {
                username = User.FindFirstValue(ClaimTypes.Name);
            }

            return username!;
        }
        protected string GetEmail()
        {
            string email = string.Empty;

            if (email != null)
            {
				email = User.FindFirstValue(ClaimTypes.Email);
            }

            return email!;
        }
    }

}
