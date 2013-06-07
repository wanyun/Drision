using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.Models;

namespace Drision.MVCFrame.Common
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DisableByAction(this HtmlHelper helper,SysFunction[] actions,string controller,string action)
        {
            if (actions != null && actions.Where(p => p.ControllerName == controller && p.ActionName == action).Count() > 1)
            {
                return new MvcHtmlString("");
            }
            else
            {
                return new MvcHtmlString("disabled='disabled'");
            }
        }

        public static bool  VisibleByAction(this HtmlHelper helper, SysFunction[] actions, string controller, string action)
        {

            if (HttpContext.Current.Session["UName"] != null && HttpContext.Current.Session["UName"].Equals("admin"))
                return true;

            if (actions != null && actions.Where(p => p.ControllerName == controller && p.ActionName == action).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}