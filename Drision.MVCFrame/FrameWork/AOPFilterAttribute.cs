using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drision.MVCFrame.FrameWork
{
    public class AOPFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BaseController bc = filterContext.Controller as BaseController;

            //是否登陆
            if (HttpContext.Current.Session["Status"] == null || !HttpContext.Current.Session["Status"].Equals("Online"))
            {
                bc.Response.Redirect("/Login/Index", true);
                return;
            }

            //判断是否有权限访问
            if (!bc.success((string)bc.RouteData.Values["controller"], (string)bc.RouteData.Values["action"]))
            {
                bc.Response.Redirect("/UserPermit/NoPermit", true);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}