using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace Lap09.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("AdminLogin") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Admins", controller = "Login", action = "Index" }));
            }    
            base.OnActionExecuting(context);
        }   
        
    }
}
