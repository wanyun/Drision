using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Drision.MVCFrame.WebControls
{
    public static class PagerHelper
    {
        public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
            return new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)).RenderPager();
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
            return new PagerBuilder(helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, htmlAttributes).RenderPager();
        }

        private static MvcHtmlString Pager(HtmlHelper helper, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(helper, (AjaxHelper)null, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, (PagerOptions)null, (IDictionary<string, object>)null);
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (PagerOptions)null, (string)null, (RouteValueDictionary)null, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, pagerOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, pagerOptions, (string)null, (RouteValueDictionary)null, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, pagerOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, pagerOptions, (string)null, (object)null, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, pagerOptions, htmlAttributes);
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, pagerOptions, (string)null, (RouteValueDictionary)null, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, pagerOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, pagerOptions, routeName, routeValues, (object)null);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, pagerOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, pagerOptions, routeName, routeValues, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, pagerOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, pagerOptions, htmlAttributes);
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, pagerOptions, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, object routeValues, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, (PagerOptions)null, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (PagerOptions)null, routeName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(helper, (PagerOptions)null, htmlAttributes);
            else
                return PagerHelper.Pager(helper, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (PagerOptions)null, routeName, routeValues, htmlAttributes);
        }

        private static MvcHtmlString AjaxPager(HtmlHelper html, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder(html, (AjaxHelper)null, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            pagerOptions.UseJqueryAjax = true;
            int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
            return new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), ajaxOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)).RenderPager();
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            pagerOptions.UseJqueryAjax = true;
            int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
            return new PagerBuilder(html, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, (PagerOptions)null, (IDictionary<string, object>)null);
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, (PagerOptions)null, (RouteValueDictionary)null, ajaxOptions, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, (PagerOptions)null, (IDictionary<string, object>)null);
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, (PagerOptions)null, (RouteValueDictionary)null, ajaxOptions, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, pagerOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, pagerOptions, (RouteValueDictionary)null, ajaxOptions, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, pagerOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, pagerOptions, (object)null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, pagerOptions, htmlAttributes);
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, pagerOptions, (RouteValueDictionary)null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, pagerOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, pagerOptions, routeValues, ajaxOptions, (object)null);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, pagerOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, pagerOptions, htmlAttributes);
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, IPagedList pagedList, string actionName, string controllerName, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList == null)
                return PagerHelper.AjaxPager(html, pagerOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.AjaxPager(html, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, actionName, controllerName, (string)null, pagerOptions, (RouteValueDictionary)null, ajaxOptions, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
            return new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, new RouteValueDictionary(routeValues), ajaxOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)).RenderPager();
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, int totalItemCount, int pageSize, int pageIndex, string actionName, string controllerName, string routeName, PagerOptions pagerOptions, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            int totalPageCount = (int)Math.Ceiling((double)totalItemCount / (double)pageSize);
            return new PagerBuilder(ajax, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes).RenderPager();
        }

        private static MvcHtmlString Pager(AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            return new PagerBuilder((HtmlHelper)null, ajax, pagerOptions, htmlAttributes).RenderPager();
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, AjaxOptions ajaxOptions)
        {
            if (pagedList != null)
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, (PagerOptions)null, (RouteValueDictionary)null, ajaxOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.Pager(ajax, (PagerOptions)null, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions)
        {
            if (pagedList != null)
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, pagerOptions, (RouteValueDictionary)null, ajaxOptions, (IDictionary<string, object>)null);
            else
                return PagerHelper.Pager(ajax, pagerOptions, (IDictionary<string, object>)null);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(ajax, pagerOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, pagerOptions, (object)null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(ajax, pagerOptions, htmlAttributes);
            else
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, (string)null, pagerOptions, (RouteValueDictionary)null, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(ajax, (PagerOptions)null, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, (PagerOptions)null, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(ajax, (PagerOptions)null, htmlAttributes);
            else
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, (PagerOptions)null, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, object routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(ajax, pagerOptions, (IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            else
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }

        public static MvcHtmlString Pager(this AjaxHelper ajax, IPagedList pagedList, string routeName, RouteValueDictionary routeValues, PagerOptions pagerOptions, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            if (pagedList == null)
                return PagerHelper.Pager(ajax, pagerOptions, htmlAttributes);
            else
                return PagerHelper.Pager(ajax, pagedList.TotalItemCount, pagedList.PageSize, pagedList.CurrentPageIndex, (string)null, (string)null, routeName, pagerOptions, routeValues, ajaxOptions, htmlAttributes);
        }
    }
}
