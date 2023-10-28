using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BenevArts.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected string GetUserId()
        {
            string userId = string.Empty;

            if (userId != null)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            }

            return userId!;
        }
      
    }
}
