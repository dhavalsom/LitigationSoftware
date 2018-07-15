using LS.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LSWebApp.Infrastructure
{
    public class LitigationAuthorizeAttribute : AuthorizeAttribute
    {
        private const string SESSION_LOGON_USER = "SessionLogonUser";

        public LitigationAuthorizeAttribute()
        {

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;

            if (this.LogonUser(httpContext) != null && this.LogonUser(httpContext).Id > 0)
            {
                result = true;
            }
            return result;
        }

        protected UserLogin LogonUser(HttpContextBase httpContext)
        {
            if (httpContext.Session[SESSION_LOGON_USER] != null)
                return httpContext.Session[SESSION_LOGON_USER] as UserLogin;
            else
                return null;

        }

        protected UserLogin LogonUser(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session[SESSION_LOGON_USER] != null)
                return filterContext.HttpContext.Session[SESSION_LOGON_USER] as UserLogin;
            else
                return null;

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            #region Unauthorized handle for security website.
            if (this.LogonUser(filterContext) == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary {
                            { "action", "UnAuthorizedAjaxCall" },
                            { "controller", "Login"},
                            { "message", "Your session has timed out. Please log back in to continue."}
                    });
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                                  new RouteValueDictionary {
                            { "action", "Index" },
                            { "controller", "Login" },
                            { "message", "Your session has timed out. Please log back in to continue."}
                    });
                }
            }
            #endregion
        }
    }
}