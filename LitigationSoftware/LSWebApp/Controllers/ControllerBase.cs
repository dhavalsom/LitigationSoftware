using System;
using System.Web;
using System.Web.Mvc;

namespace LSWebApp.Controllers
{
    public class ControllerBase : Controller
    {
        private string _requestedAction;
        private string _requestedController;
        private bool? _isAjaxRequest;

        #region Private Variables
        protected const string SESSION_LOGON_USER = "SessionLogonUser";
        #endregion
        
        #region Constructors
        public ControllerBase()
        {
        }
        #endregion    

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");

            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            System.Web.HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            System.Web.HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();

            try
            {
                _requestedAction = filterContext.ActionDescriptor.ActionName;
                _requestedController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                if (filterContext.HttpContext != null && filterContext.HttpContext.Request != null)
                {
                    _isAjaxRequest = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
                }
            }
            catch
            {
            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);            
        }
    }
}