using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public abstract class SecureController : Controller
{
    public override void OnActionExecuting(
        ActionExecutingContext context)
    {
        var token =
            HttpContext.Session.GetString("JWT");

        if (string.IsNullOrEmpty(token))
        {
            context.Result =
                new RedirectToActionResult(
                    "Login",
                    "Account",
                    null);
        }

        base.OnActionExecuting(context);
    }
}