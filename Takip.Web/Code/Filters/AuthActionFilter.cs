using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Takip.Web.Controllers;

namespace Takip.Web.Code.Filters
{
    public class AuthActionFilter : ActionFilterAttribute, IAuthorizationFilter
    {

        public string Role;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!string.IsNullOrEmpty(Role))
            {
                bool isAuthorized = Role.Split(',').Contains(Repo.Session.Role);
                if (!isAuthorized)
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = "Unauthorized",
                        controller = "Account",
                        area = ""
                    }));
            }
            else if (string.IsNullOrEmpty(Repo.Session.Email))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Account",
                    area = ""
                }));
            }
        }

        /*
        * Action çalışmadan önce
        */
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
        /*
        * Action çalıştıktan sonra
        */
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        /*
        * Tam response'u kullanıcıya göndermeden önce çalıştırılır
        */
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
        /*
        * Sonucu gönderdik ve bu metod çalışıyor
        */
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
