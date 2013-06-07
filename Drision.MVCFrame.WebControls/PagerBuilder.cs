using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Drision.MVCFrame.WebControls
{
    internal class PagerBuilder
    {
        private const string CopyrightText = "\r\n<!--MvcPager 1.5 for ASP.NET MVC 3.0 © 2009-2011 Webdiyer (http://www.webdiyer.com)-->\r\n";
        private const string ScriptPageIndexName = "*_MvcPager_PageIndex_*";
        private const string GoToPageScript = "function _MvcPager_GoToPage(_pib,_mp){var pageIndex;if(_pib.tagName==\"SELECT\"){pageIndex=_pib.options[_pib.selectedIndex].value;}else{pageIndex=_pib.value;var r=new RegExp(\"^\\\\s*(\\\\d+)\\\\s*$\");if(!r.test(pageIndex)){alert(\"%InvalidPageIndexErrorMessage%\");return;}else if(RegExp.$1<1||RegExp.$1>_mp){alert(\"%PageIndexOutOfRangeErrorMessage%\");return;}}var _hl=document.getElementById(_pib.id+'link').childNodes[0];var _lh=_hl.href;_hl.href=_lh.replace('*_MvcPager_PageIndex_*',pageIndex);if(_hl.click){_hl.click();}else{var evt=document.createEvent('MouseEvents');evt.initEvent('click',true,true);_hl.dispatchEvent(evt);}_hl.href=_lh;}";
        private const string KeyDownScript = @"function _MvcPager_Keydown(e){var _kc,_pib;if(window.event){_kc=e.keyCode;_pib=e.srcElement;}else if(e.which){_kc=e.which;_pib=e.target;}var validKey=(_kc==8||_kc==46||_kc==37||_kc==39||(_kc>=48&&_kc<=57)||(_kc>=96&&_kc<=105));if(!validKey){if(_kc==13){ _MvcPager_GoToPage(_pib,%TotalPageCount%);}if(e.preventDefault){e.preventDefault();}else{event.returnValue=false;}}}";
        private readonly HtmlHelper _html;
        private readonly AjaxHelper _ajax;
        private readonly string _actionName;
        private readonly string _controllerName;
        private readonly int _totalPageCount;
        private readonly int _pageIndex;
        private readonly PagerOptions _pagerOptions;
        private readonly RouteValueDictionary _routeValues;
        private readonly string _routeName;
        private readonly int _startPageIndex;
        private readonly int _endPageIndex;
        private readonly bool _msAjaxPaging;
        private readonly AjaxOptions _ajaxOptions;
        private IDictionary<string, object> _htmlAttributes;

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, PagerOptions pagerOptions, IDictionary<string, object> htmlAttributes)
        {
            this._totalPageCount = 1;
            this._startPageIndex = 1;
            this._endPageIndex = 1;

            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            this._html = html;
            this._ajax = ajax;
            this._pagerOptions = pagerOptions;
            this._htmlAttributes = htmlAttributes;
        }

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            this._totalPageCount = 1;
            this._startPageIndex = 1;
            this._endPageIndex = 1;

            this._msAjaxPaging = ajax != null;
            if (string.IsNullOrEmpty(actionName))
                actionName = ajax == null ? (string)html.ViewContext.RouteData.Values["action"] : (string)ajax.ViewContext.RouteData.Values["action"];
            if (string.IsNullOrEmpty(controllerName))
                controllerName = ajax == null ? (string)html.ViewContext.RouteData.Values["controller"] : (string)ajax.ViewContext.RouteData.Values["controller"];
            if (pagerOptions == null)
                pagerOptions = new PagerOptions();
            this._html = html;
            this._ajax = ajax;
            this._actionName = actionName;
            this._controllerName = controllerName;
            this._totalPageCount = pagerOptions.MaxPageIndex == 0 || pagerOptions.MaxPageIndex > totalPageCount ? totalPageCount : pagerOptions.MaxPageIndex;
            this._pageIndex = pageIndex;
            this._pagerOptions = pagerOptions;
            this._routeName = routeName;
            this._routeValues = routeValues;
            this._ajaxOptions = ajaxOptions;
            this._htmlAttributes = htmlAttributes;
            this._startPageIndex = pageIndex - pagerOptions.NumericPagerItemCount / 2;
            if (this._startPageIndex + pagerOptions.NumericPagerItemCount > this._totalPageCount)
                this._startPageIndex = this._totalPageCount + 1 - pagerOptions.NumericPagerItemCount;
            if (this._startPageIndex < 1)
                this._startPageIndex = 1;
            this._endPageIndex = this._startPageIndex + this._pagerOptions.NumericPagerItemCount - 1;
            if (this._endPageIndex <= this._totalPageCount)
                return;
            this._endPageIndex = this._totalPageCount;
        }

        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
            : this(helper, (AjaxHelper)null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, (AjaxOptions)null, htmlAttributes)
        {
        }

        internal PagerBuilder(AjaxHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
            : this((HtmlHelper)null, helper, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes)
        {
        }

        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerOptions pagerOptions, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
            : this(helper, (AjaxHelper)null, actionName, controllerName, totalPageCount, pageIndex, pagerOptions, routeName, routeValues, ajaxOptions, htmlAttributes)
        {
        }

        private void AddPrevious(ICollection<PagerItem> results)
        {
            PagerItem pagerItem = new PagerItem(this._pagerOptions.PrevPageText, this._pageIndex - 1, this._pageIndex == 1, PagerItemType.PrevPage);
            if (pagerItem.Disabled && (!pagerItem.Disabled || !this._pagerOptions.ShowDisabledPagerItems))
                return;
            results.Add(pagerItem);
        }

        private void AddFirst(ICollection<PagerItem> results)
        {
            PagerItem pagerItem = new PagerItem(this._pagerOptions.FirstPageText, 1, this._pageIndex == 1, PagerItemType.FirstPage);
            if (pagerItem.Disabled && (!pagerItem.Disabled || !this._pagerOptions.ShowDisabledPagerItems))
                return;
            results.Add(pagerItem);
        }

        private void AddMoreBefore(ICollection<PagerItem> results)
        {
            if (this._startPageIndex <= 1 || !this._pagerOptions.ShowMorePagerItems)
                return;
            int pageIndex = this._startPageIndex - 1;
            if (pageIndex < 1)
                pageIndex = 1;
            PagerItem pagerItem = new PagerItem(this._pagerOptions.MorePageText, pageIndex, false, PagerItemType.MorePage);
            results.Add(pagerItem);
        }

        private void AddPageNumbers(ICollection<PagerItem> results)
        {
            for (int pageIndex = this._startPageIndex; pageIndex <= this._endPageIndex; ++pageIndex)
            {
                string text = pageIndex.ToString();
                if (pageIndex == this._pageIndex && !string.IsNullOrEmpty(this._pagerOptions.CurrentPageNumberFormatString))
                    text = string.Format(this._pagerOptions.CurrentPageNumberFormatString, (object)text);
                else if (!string.IsNullOrEmpty(this._pagerOptions.PageNumberFormatString))
                    text = string.Format(this._pagerOptions.PageNumberFormatString, (object)text);
                PagerItem pagerItem = new PagerItem(text, pageIndex, false, PagerItemType.NumericPage);
                results.Add(pagerItem);
            }
        }

        private void AddMoreAfter(ICollection<PagerItem> results)
        {
            if (this._endPageIndex >= this._totalPageCount)
                return;
            int pageIndex = this._startPageIndex + this._pagerOptions.NumericPagerItemCount;
            if (pageIndex > this._totalPageCount)
                pageIndex = this._totalPageCount;
            PagerItem pagerItem = new PagerItem(this._pagerOptions.MorePageText, pageIndex, false, PagerItemType.MorePage);
            results.Add(pagerItem);
        }

        private void AddNext(ICollection<PagerItem> results)
        {
            PagerItem pagerItem = new PagerItem(this._pagerOptions.NextPageText, this._pageIndex + 1, this._pageIndex >= this._totalPageCount, PagerItemType.NextPage);
            if (pagerItem.Disabled && (!pagerItem.Disabled || !this._pagerOptions.ShowDisabledPagerItems))
                return;
            results.Add(pagerItem);
        }

        private void AddLast(ICollection<PagerItem> results)
        {
            PagerItem pagerItem = new PagerItem(this._pagerOptions.LastPageText, this._totalPageCount, this._pageIndex >= this._totalPageCount, PagerItemType.LastPage);
            if (pagerItem.Disabled && (!pagerItem.Disabled || !this._pagerOptions.ShowDisabledPagerItems))
                return;
            results.Add(pagerItem);
        }

        private string GenerateUrl(int pageIndex)
        {
            if (pageIndex > this._totalPageCount || pageIndex == this._pageIndex)
                return (string)null;
            RouteValueDictionary currentRouteValues = this.GetCurrentRouteValues(this._html.ViewContext);
            currentRouteValues[this._pagerOptions.PageIndexParameterName] = pageIndex != 0 ? (object)pageIndex : (object)"*_MvcPager_PageIndex_*";
            UrlHelper urlHelper = new UrlHelper(this._html.ViewContext.RequestContext);
            if (!string.IsNullOrEmpty(this._routeName))
                return urlHelper.RouteUrl(this._routeName, currentRouteValues);
            else
                return urlHelper.RouteUrl(currentRouteValues);
        }

        internal MvcHtmlString RenderPager()
        {
            if (this._totalPageCount <= 1 && this._pagerOptions.AutoHide)
                return MvcHtmlString.Create("\r\n<!--MvcPager 1.5 for ASP.NET MVC 3.0 © 2009-2011 Webdiyer (http://www.webdiyer.com)-->\r\n");
            if (this._pageIndex > this._totalPageCount && this._totalPageCount > 0 || this._pageIndex < 1)
                return MvcHtmlString.Create(string.Format("{0}<div style=\"color:red;font-weight:bold\">{1}</div>{0}", (object)"\r\n<!--MvcPager 1.5 for ASP.NET MVC 3.0 © 2009-2011 Webdiyer (http://www.webdiyer.com)-->\r\n", (object)this._pagerOptions.PageIndexOutOfRangeErrorMessage));
            List<PagerItem> list = new List<PagerItem>();
            if (this._pagerOptions.ShowFirstLast)
                this.AddFirst((ICollection<PagerItem>)list);
            if (this._pagerOptions.ShowPrevNext)
                this.AddPrevious((ICollection<PagerItem>)list);
            if (this._pagerOptions.ShowNumericPagerItems)
            {
                if (this._pagerOptions.AlwaysShowFirstLastPageNumber && this._startPageIndex > 1)
                    list.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));
                if (this._pagerOptions.ShowMorePagerItems)
                    this.AddMoreBefore((ICollection<PagerItem>)list);
                this.AddPageNumbers((ICollection<PagerItem>)list);
                if (this._pagerOptions.ShowMorePagerItems)
                    this.AddMoreAfter((ICollection<PagerItem>)list);
                if (this._pagerOptions.AlwaysShowFirstLastPageNumber && this._endPageIndex < this._totalPageCount)
                    list.Add(new PagerItem(this._totalPageCount.ToString(), this._totalPageCount, false, PagerItemType.NumericPage));
            }
            if (this._pagerOptions.ShowPrevNext)
                this.AddNext((ICollection<PagerItem>)list);
            if (this._pagerOptions.ShowFirstLast)
                this.AddLast((ICollection<PagerItem>)list);
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder sbPagination = new StringBuilder();
            if (this._msAjaxPaging)
            {
                foreach (PagerItem pagerItem in list)
                    stringBuilder.Append((object)this.GenerateMsAjaxPagerElement(pagerItem));
            }
            else if (this._pagerOptions.UseJqueryAjax)
            {
                foreach (PagerItem pagerItem in list)
                    stringBuilder.Append((object)this.GenerateJqAjaxPagerElement(pagerItem));
            }
            else
            {
                foreach (PagerItem pagerItem in list)
                    stringBuilder.Append((object)this.GeneratePagerElement(pagerItem));
            }
            TagBuilder tagBuilder = new TagBuilder(this._pagerOptions.ContainerTagName);
            if (!string.IsNullOrEmpty(this._pagerOptions.Id))
                tagBuilder.GenerateId(this._pagerOptions.Id);
            if (!string.IsNullOrEmpty(this._pagerOptions.CssClass))
                tagBuilder.AddCssClass(this._pagerOptions.CssClass);
            if (!string.IsNullOrEmpty(this._pagerOptions.HorizontalAlign))
            {
                string str = "text-align:" + this._pagerOptions.HorizontalAlign.ToLower();
                if (this._htmlAttributes == null)
                {
                    PagerBuilder pagerBuilder = this;
                    RouteValueDictionary routeValueDictionary1 = new RouteValueDictionary();
                    routeValueDictionary1.Add("style", (object)str);
                    RouteValueDictionary routeValueDictionary2 = routeValueDictionary1;
                    pagerBuilder._htmlAttributes = (IDictionary<string, object>)routeValueDictionary2;
                }
                else if (this._htmlAttributes.Keys.Contains("style"))
                {
                    IDictionary<string, object> dictionary;
                    (dictionary = this._htmlAttributes)["style"] = (object)((string)dictionary["style"] + (object)";" + str);
                }
            }
            tagBuilder.MergeAttributes<string, object>(this._htmlAttributes, true);
            sbPagination.Append("<ul>" + stringBuilder.ToString() + "</ul>");
            stringBuilder.Length = 0;
            string pagerScript = string.Empty;
            if (this._pagerOptions.ShowPageIndexBox)
                stringBuilder.Append(this.BuildGoToPageSection(ref pagerScript));
            else
                stringBuilder.Length -= this._pagerOptions.SeparatorHtml.Length;
            tagBuilder.InnerHtml = ((object)sbPagination).ToString();
            if (!string.IsNullOrEmpty(pagerScript))
                pagerScript = "<script type=\"text/javascript\">//<![CDATA[\r\n" + pagerScript + "\r\n//]]>\r\n</script>";
            return MvcHtmlString.Create("\r\n<!--MvcPager 1.5 for ASP.NET MVC 3.0 © 2009-2011 Webdiyer (http://www.webdiyer.com)-->\r\n" + pagerScript + tagBuilder.ToString(TagRenderMode.Normal) + stringBuilder+"\r\n<!--MvcPager 1.5 for ASP.NET MVC 3.0 © 2009-2011 Webdiyer (http://www.webdiyer.com)-->\r\n");
        }

        private string BuildGoToPageSection(ref string pagerScript)
        {
            ViewContext viewContext = this._msAjaxPaging ? this._ajax.ViewContext : this._html.ViewContext;
            int result;
            if (int.TryParse((string)viewContext.HttpContext.Items[(object)"_MvcPager_ControlIndex"], out result))
                ++result;
            viewContext.HttpContext.Items[(object)"_MvcPager_ControlIndex"] = (object)result.ToString();
            string str1 = "_MvcPager_Ctrl" + (object)result;
            string str2 = this.GenerateAnchor(new PagerItem("0", 0, false, PagerItemType.NumericPage));
            if (result == 0)
            {
                // ISSUE: explicit reference operation
                // ISSUE: variable of a reference type
                string local = pagerScript;
                // ISSUE: explicit reference operation
                string str3 = pagerScript + "function _MvcPager_Keydown(e){var _kc,_pib;if(window.event){_kc=e.keyCode;_pib=e.srcElement;}else if(e.which){_kc=e.which;_pib=e.target;}var validKey=(_kc==8||_kc==46||_kc==37||_kc==39||(_kc>=48&&_kc<=57)||(_kc>=96&&_kc<=105));if(!validKey){if(_kc==13){ _MvcPager_GoToPage(_pib,%TotalPageCount%);}if(e.preventDefault){e.preventDefault();}else{event.returnValue=false;}}}".Replace("%TotalPageCount%", this._totalPageCount.ToString()) + "function _MvcPager_GoToPage(_pib,_mp){var pageIndex;if(_pib.tagName==\"SELECT\"){pageIndex=_pib.options[_pib.selectedIndex].value;}else{pageIndex=_pib.value;var r=new RegExp(\"^\\\\s*(\\\\d+)\\\\s*$\");if(!r.test(pageIndex)){alert(\"%InvalidPageIndexErrorMessage%\");return;}else if(RegExp.$1<1||RegExp.$1>_mp){alert(\"%PageIndexOutOfRangeErrorMessage%\");return;}}var _hl=document.getElementById(_pib.id+'link').childNodes[0];var _lh=_hl.href;_hl.href=_lh.replace('*_MvcPager_PageIndex_*',pageIndex);if(_hl.click){_hl.click();}else{var evt=document.createEvent('MouseEvents');evt.initEvent('click',true,true);_hl.dispatchEvent(evt);}_hl.href=_lh;}".Replace("%InvalidPageIndexErrorMessage%", this._pagerOptions.InvalidPageIndexErrorMessage).Replace("%PageIndexOutOfRangeErrorMessage%", this._pagerOptions.PageIndexOutOfRangeErrorMessage);
                // ISSUE: explicit reference operation
                pagerScript = str3;
            }
            string str4 = (string)null;
            if (!this._pagerOptions.ShowGoButton)
                str4 = " onchange=\"_MvcPager_GoToPage(this," + (object)this._totalPageCount + ")\"";
            StringBuilder stringBuilder = new StringBuilder();
            if (this._pagerOptions.PageIndexBoxType == PageIndexBoxType.DropDownList)
            {
                int num1 = this._pageIndex - this._pagerOptions.MaximumPageIndexItems / 2;
                if (num1 + this._pagerOptions.MaximumPageIndexItems > this._totalPageCount)
                    num1 = this._totalPageCount + 1 - this._pagerOptions.MaximumPageIndexItems;
                if (num1 < 1)
                    num1 = 1;
                int num2 = num1 + this._pagerOptions.MaximumPageIndexItems - 1;
                if (num2 > this._totalPageCount)
                    num2 = this._totalPageCount;
                stringBuilder.AppendFormat("<select id=\"{0}\"{1}>", (object)(str1 + "_pib"), (object)str4);
                for (int index = num1; index <= num2; ++index)
                {
                    stringBuilder.AppendFormat("<option value=\"{0}\"", (object)index);
                    if (index == this._pageIndex)
                        stringBuilder.Append(" selected=\"selected\"");
                    stringBuilder.AppendFormat(">{0}</option>", (object)index);
                }
                stringBuilder.Append("</select>");
            }
            else
                stringBuilder.AppendFormat("<span class=\"add-on\"><i class=\"icon-th-list\"></i></span><input type=\"text\" id=\"{0}\" class=\"input-mini\" value=\"{1}\" onkeydown=\"_MvcPager_Keydown(event)\"{2}/>", (object)(str1 + "_pib"), (object)this._pageIndex, (object)str4);

            if (!string.IsNullOrEmpty(this._pagerOptions.PageIndexBoxWrapperFormatString))
                stringBuilder = new StringBuilder(string.Format(this._pagerOptions.PageIndexBoxWrapperFormatString, (object)stringBuilder));
            if (this._pagerOptions.ShowGoButton)
                stringBuilder.AppendFormat("<input type=\"button\" value=\"{0}\"  class=\"btn  btn-info\" onclick=\"_MvcPager_GoToPage(document.getElementById('{1}')," + (object)this._totalPageCount + ")\"/>", (object)this._pagerOptions.GoButtonText, (object)(str1 + "_pib"));

            stringBuilder.AppendFormat("<span id=\"{0}\" style=\"display:none;width:0px;height:0px\">{1}</span>", (object)(str1 + "_piblink"), (object)str2);
            //加入div input-prepend
            stringBuilder = new StringBuilder(string.Format("<div class=\"input-prepend\">{0}</div>", (object)stringBuilder));
            return !string.IsNullOrEmpty(this._pagerOptions.GoToPageSectionWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString) ? string.Format(this._pagerOptions.GoToPageSectionWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, (object)stringBuilder) : ((object)stringBuilder).ToString();
        }

        private string GenerateAnchor(PagerItem item)
        {
            if (this._msAjaxPaging)
            {
                RouteValueDictionary currentRouteValues = this.GetCurrentRouteValues(this._ajax.ViewContext);
                currentRouteValues[this._pagerOptions.PageIndexParameterName] = item.PageIndex != 0 ? (object)item.PageIndex : (object)"*_MvcPager_PageIndex_*";
                if (!string.IsNullOrEmpty(this._routeName))
                    return AjaxExtensions.RouteLink(this._ajax, item.Text, this._routeName, currentRouteValues, this._ajaxOptions).ToString();
                else
                    return AjaxExtensions.RouteLink(this._ajax, item.Text, currentRouteValues, this._ajaxOptions).ToString();
            }
            else
            {
                string str = this.GenerateUrl(item.PageIndex);
                if (!this._pagerOptions.UseJqueryAjax)
                    return "<a href=\"" + str + "\" onclick=\"window.open(this.attributes.getNamedItem('href').value,'_self')\"></a>";
                if (this._html.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    TagBuilder tagBuilder1 = new TagBuilder("a");
                    tagBuilder1.InnerHtml = item.Text;
                    TagBuilder tagBuilder2 = tagBuilder1;
                    tagBuilder2.MergeAttribute("href", str);
                    tagBuilder2.MergeAttributes<string, object>(this._ajaxOptions.ToUnobtrusiveHtmlAttributes());
                    if (!string.IsNullOrEmpty(str))
                        return tagBuilder2.ToString(TagRenderMode.Normal);
                    else
                        return this._html.Encode(item.Text);
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (!string.IsNullOrEmpty(this._ajaxOptions.OnFailure) || !string.IsNullOrEmpty(this._ajaxOptions.OnBegin) || !string.IsNullOrEmpty(this._ajaxOptions.OnComplete) && this._ajaxOptions.HttpMethod.ToUpper() != "GET")
                    {
                        stringBuilder.Append("$.ajax({type:'").Append(this._ajaxOptions.HttpMethod.ToUpper() == "GET" ? "get" : "post");
                        stringBuilder.Append("',url:$(this).attr('href'),success:function(data,status,xhr){$('#");
                        stringBuilder.Append(this._ajaxOptions.UpdateTargetId).Append("').html(data);}");
                        if (!string.IsNullOrEmpty(this._ajaxOptions.OnFailure))
                            stringBuilder.Append(",error:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnFailure));
                        if (!string.IsNullOrEmpty(this._ajaxOptions.OnBegin))
                            stringBuilder.Append(",beforeSend:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnBegin));
                        if (!string.IsNullOrEmpty(this._ajaxOptions.OnComplete))
                            stringBuilder.Append(",complete:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnComplete));
                        stringBuilder.Append("});return false;");
                    }
                    else if (this._ajaxOptions.HttpMethod.ToUpper() == "GET")
                    {
                        stringBuilder.Append("$('#").Append(this._ajaxOptions.UpdateTargetId);
                        stringBuilder.Append("').load($(this).attr('href')");
                        if (!string.IsNullOrEmpty(this._ajaxOptions.OnComplete))
                            stringBuilder.Append(",").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnComplete));
                        stringBuilder.Append(");return false;");
                    }
                    else
                    {
                        stringBuilder.Append("$.post($(this).attr('href'), function(data) {$('#");
                        stringBuilder.Append(this._ajaxOptions.UpdateTargetId);
                        stringBuilder.Append("').html(data);});return false;");
                    }
                    if (string.IsNullOrEmpty(str))
                        return this._html.Encode(item.Text);
                    return string.Format((IFormatProvider)CultureInfo.InvariantCulture, "<a href=\"{0}\" onclick=\"{1}\">{2}</a>", (object)str, (object)stringBuilder, (object)item.Text);
                }
            }
        }

        private MvcHtmlString GeneratePagerElement(PagerItem item)
        {
            string str = this.GenerateUrl(item.PageIndex);
            if (item.Disabled)
                return this.CreateWrappedPagerElement(item, string.Format("<li><a disabled=\"disabled\">{0}</a></li>", (object)item.Text));
            else
                return this.CreateWrappedPagerElement(item, string.IsNullOrEmpty(str) ? string.Format("<li class='active'  ><a  href='#'>{0}</a><li>", this._html.Encode(item.Text)) : string.Format("<li><a  href='{0}'>{1}</a><li>", (object)str, (object)item.Text));
        }

        private MvcHtmlString GenerateJqAjaxPagerElement(PagerItem item)
        {
            if (item.Disabled)
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", (object)item.Text));
            else
                return this.CreateWrappedPagerElement(item, this.GenerateAnchor(item));
        }

        private MvcHtmlString GenerateMsAjaxPagerElement(PagerItem item)
        {
            if (item.PageIndex == this._pageIndex && !item.Disabled)
                return this.CreateWrappedPagerElement(item, item.Text);
            if (item.Disabled)
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", (object)item.Text));
            if (item.PageIndex < 1 || item.PageIndex > this._totalPageCount)
                return (MvcHtmlString)null;
            else
                return this.CreateWrappedPagerElement(item, this.GenerateAnchor(item));
        }

        private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el)
        {
            string str = el;
            switch (item.Type)
            {
                case PagerItemType.FirstPage:
                case PagerItemType.NextPage:
                case PagerItemType.PrevPage:
                case PagerItemType.LastPage:
                    if (!string.IsNullOrEmpty(this._pagerOptions.NavigationPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerOptions.NavigationPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, (object)el);
                        break;
                    }
                    else
                        break;
                case PagerItemType.MorePage:
                    if (!string.IsNullOrEmpty(this._pagerOptions.MorePagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerOptions.MorePagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, (object)el);
                        break;
                    }
                    else
                        break;
                case PagerItemType.NumericPage:
                    if (item.PageIndex == this._pageIndex && (!string.IsNullOrEmpty(this._pagerOptions.CurrentPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString)))
                    {
                        str = string.Format(this._pagerOptions.CurrentPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, (object)el);
                        break;
                    }
                    else if (!string.IsNullOrEmpty(this._pagerOptions.NumericPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerOptions.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerOptions.NumericPagerItemWrapperFormatString ?? this._pagerOptions.PagerItemWrapperFormatString, (object)el);
                        break;
                    }
                    else
                        break;
            }
            return MvcHtmlString.Create(str + this._pagerOptions.SeparatorHtml);
        }

        private RouteValueDictionary GetCurrentRouteValues(ViewContext viewContext)
        {
            RouteValueDictionary routeValueDictionary = this._routeValues ?? new RouteValueDictionary();
            NameValueCollection queryString = viewContext.HttpContext.Request.QueryString;
            if (queryString != null && queryString.Count > 0)
            {
                string[] array = new string[3]
        {
          "x-requested-with",
          "xmlhttprequest",
          this._pagerOptions.PageIndexParameterName.ToLower()
        };
                foreach (string index in queryString.Keys)
                {
                    if (!string.IsNullOrEmpty(index) && Array.IndexOf<string>(array, index.ToLower()) < 0)
                        routeValueDictionary[index] = (object)queryString[index];
                }
            }
            routeValueDictionary["action"] = (object)this._actionName;
            routeValueDictionary["controller"] = (object)this._controllerName;
            return routeValueDictionary;
        }
    }
}
