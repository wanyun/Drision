using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Drision.MVCFrame.WebControls
{
    public class PagerOptions
    {
        private string _containerTagName;
        [CompilerGenerated]
        private bool _AutoHide;
        [CompilerGenerated]
        private string _PageIndexOutOfRangeErrorMessage;
        [CompilerGenerated]
        private string _InvalidPageIndexErrorMessage;
        [CompilerGenerated]
        private string _PageIndexParameterName;
        [CompilerGenerated]
        private bool _ShowPageIndexBox;
        [CompilerGenerated]
        private PageIndexBoxType _PageIndexBoxType;
        [CompilerGenerated]
        private int _MaximumPageIndexItems;
        [CompilerGenerated]
        private bool _ShowGoButton;
        [CompilerGenerated]
        private string _GoButtonText;
        [CompilerGenerated]
        private string _PageNumberFormatString;
        [CompilerGenerated]
        private string _CurrentPageNumberFormatString;
        [CompilerGenerated]
        private string _PagerItemWrapperFormatString;
        [CompilerGenerated]
        private string _NumericPagerItemWrapperFormatString;
        [CompilerGenerated]
        private string _CurrentPagerItemWrapperFormatString;
        [CompilerGenerated]
        private string _NavigationPagerItemWrapperFormatString;
        [CompilerGenerated]
        private string _MorePagerItemWrapperFormatString;
        [CompilerGenerated]
        private string _PageIndexBoxWrapperFormatString;
        [CompilerGenerated]
        private string _GoToPageSectionWrapperFormatString;
        [CompilerGenerated]
        private bool _AlwaysShowFirstLastPageNumber;
        [CompilerGenerated]
        private int _NumericPagerItemCount;
        [CompilerGenerated]
        private bool _ShowPrevNext;
        [CompilerGenerated]
        private string _PrevPageText;
        [CompilerGenerated]
        private string _NextPageText;
        [CompilerGenerated]
        private bool _ShowNumericPagerItems;
        [CompilerGenerated]
        private bool _ShowFirstLast;
        [CompilerGenerated]
        private string _FirstPageText;
        [CompilerGenerated]
        private string _LastPageText;
        [CompilerGenerated]
        private bool _ShowMorePagerItems;
        [CompilerGenerated]
        private string _MorePageText;
        [CompilerGenerated]
        private string _Id;
        [CompilerGenerated]
        private string _HorizontalAlign;
        [CompilerGenerated]
        private string _CssClass;
        [CompilerGenerated]
        private bool _ShowDisabledPagerItems;
        [CompilerGenerated]
        private string _SeparatorHtml;
        [CompilerGenerated]
        private int _MaxPageIndex;
        [CompilerGenerated]
        private bool _UseJqueryAjax;

        public bool AutoHide
        {
            get
            {
                return this._AutoHide;
            }
            set
            {
                this._AutoHide = value;
            }
        }

        public string PageIndexOutOfRangeErrorMessage
        {
            get
            {
                return this._PageIndexOutOfRangeErrorMessage;
            }
            set
            {
                this._PageIndexOutOfRangeErrorMessage = value;
            }
        }

        public string InvalidPageIndexErrorMessage
        {
            get
            {
                return this._InvalidPageIndexErrorMessage;
            }
            set
            {
                this._InvalidPageIndexErrorMessage = value;
            }
        }

        public string PageIndexParameterName
        {
            get
            {
                return this._PageIndexParameterName;
            }
            set
            {
                this._PageIndexParameterName = value;
            }
        }

        public bool ShowPageIndexBox
        {
            get
            {
                return this._ShowPageIndexBox;
            }
            set
            {
                this._ShowPageIndexBox = value;
            }
        }

        public PageIndexBoxType PageIndexBoxType
        {
            get
            {
                return this._PageIndexBoxType;
            }
            set
            {
                this._PageIndexBoxType = value;
            }
        }

        public int MaximumPageIndexItems
        {
            get
            {
                return this._MaximumPageIndexItems;
            }
            set
            {
                this._MaximumPageIndexItems = value;
            }
        }

        public bool ShowGoButton
        {
            get
            {
                return this._ShowGoButton;
            }
            set
            {
                this._ShowGoButton = value;
            }
        }

        public string GoButtonText
        {
            get
            {
                return this._GoButtonText;
            }
            set
            {
                this._GoButtonText = value;
            }
        }

        public string PageNumberFormatString
        {
            get
            {
                return this._PageNumberFormatString;
            }
            set
            {
                this._PageNumberFormatString = value;
            }
        }

        public string CurrentPageNumberFormatString
        {
            get
            {
                return this._CurrentPageNumberFormatString;
            }
            set
            {
                this._CurrentPageNumberFormatString = value;
            }
        }

        public string ContainerTagName
        {
            get
            {
                return this._containerTagName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("ContainerTagName不能为null或空字符串", "ContainerTagName");
                this._containerTagName = value;
            }
        }

        public string PagerItemWrapperFormatString
        {
            get
            {
                return this._PagerItemWrapperFormatString;
            }
            set
            {
                this._PagerItemWrapperFormatString = value;
            }
        }

        public string NumericPagerItemWrapperFormatString
        {
            get
            {
                return this._NumericPagerItemWrapperFormatString;
            }
            set
            {
                this._NumericPagerItemWrapperFormatString = value;
            }
        }

        public string CurrentPagerItemWrapperFormatString
        {
            get
            {
                return this._CurrentPagerItemWrapperFormatString;
            }
            set
            {
                this._CurrentPagerItemWrapperFormatString = value;
            }
        }

        public string NavigationPagerItemWrapperFormatString
        {
            get
            {
                return this._NavigationPagerItemWrapperFormatString;
            }
            set
            {
                this._NavigationPagerItemWrapperFormatString = value;
            }
        }

        public string MorePagerItemWrapperFormatString
        {
            get
            {
                return this._MorePagerItemWrapperFormatString;
            }
            set
            {
                this._MorePagerItemWrapperFormatString = value;
            }
        }

        public string PageIndexBoxWrapperFormatString
        {
            get
            {
                return this._PageIndexBoxWrapperFormatString;
            }
            set
            {
                this._PageIndexBoxWrapperFormatString = value;
            }
        }

        public string GoToPageSectionWrapperFormatString
        {
            get
            {
                return this._GoToPageSectionWrapperFormatString;
            }
            set
            {
                this._GoToPageSectionWrapperFormatString = value;
            }
        }

        public bool AlwaysShowFirstLastPageNumber
        {
            get
            {
                return this._AlwaysShowFirstLastPageNumber;
            }
            set
            {
                this._AlwaysShowFirstLastPageNumber = value;
            }
        }

        public int NumericPagerItemCount
        {
            get
            {
                return this._NumericPagerItemCount;
            }
            set
            {
                this._NumericPagerItemCount = value;
            }
        }

        public bool ShowPrevNext
        {
            get
            {
                return this._ShowPrevNext;
            }
            set
            {
                this._ShowPrevNext = value;
            }
        }

        public string PrevPageText
        {
            get
            {
                return this._PrevPageText;
            }
            set
            {
                this._PrevPageText = value;
            }
        }

        public string NextPageText
        {
            get
            {
                return this._NextPageText;
            }
            set
            {
                this._NextPageText = value;
            }
        }

        public bool ShowNumericPagerItems
        {
            get
            {
                return this._ShowNumericPagerItems;
            }
            set
            {
                this._ShowNumericPagerItems = value;
            }
        }

        public bool ShowFirstLast
        {
            get
            {
                return this._ShowFirstLast;
            }
            set
            {
                this._ShowFirstLast = value;
            }
        }

        public string FirstPageText
        {
            get
            {
                return this._FirstPageText;
            }
            set
            {
                this._FirstPageText = value;
            }
        }

        public string LastPageText
        {
            get
            {
                return this._LastPageText;
            }
            set
            {
                this._LastPageText = value;
            }
        }

        public bool ShowMorePagerItems
        {
            get
            {
                return this._ShowMorePagerItems;
            }
            set
            {
                this._ShowMorePagerItems = value;
            }
        }

        public string MorePageText
        {
            get
            {
                return this._MorePageText;
            }
            set
            {
                this._MorePageText = value;
            }
        }

        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        public string HorizontalAlign
        {
            get
            {
                return this._HorizontalAlign;
            }
            set
            {
                this._HorizontalAlign = value;
            }
        }

        public string CssClass
        {
            get
            {
                return this._CssClass;
            }
            set
            {
                this._CssClass = value;
            }
        }

        public bool ShowDisabledPagerItems
        {
            get
            {
                return this._ShowDisabledPagerItems;
            }
            set
            {
                this._ShowDisabledPagerItems = value;
            }
        }

        public string SeparatorHtml
        {
            get
            {
                return this._SeparatorHtml;
            }
            set
            {
                this._SeparatorHtml = value;
            }
        }

        public int MaxPageIndex
        {
            get
            {
                return this._MaxPageIndex;
            }
            set
            {
                this._MaxPageIndex = value;
            }
        }

        internal bool UseJqueryAjax
        {
            get
            {
                return this._UseJqueryAjax;
            }
            set
            {
                this._UseJqueryAjax = value;
            }
        }

        public PagerOptions()
        {

            this.AutoHide = true;
            this.PageIndexParameterName = "pageIndex";
            this.NumericPagerItemCount = 10;
            this.AlwaysShowFirstLastPageNumber = false;
            this.ShowPrevNext = true;
            this.PrevPageText = "上一页";
            this.NextPageText = "下一页";
            this.ShowNumericPagerItems = true;
            this.ShowFirstLast = true;
            this.FirstPageText = "首页";
            this.LastPageText = "尾页";
            this.ShowMorePagerItems = true;
            this.MorePageText = "...";
            this.ShowDisabledPagerItems = true;
            this.SeparatorHtml = "&nbsp;&nbsp;";
            this.UseJqueryAjax = false;
            this.ShowPageIndexBox = false;
            this.ShowGoButton = true;
            this.PageIndexBoxType = PageIndexBoxType.TextBox;
            this.MaximumPageIndexItems = 80;
            this.GoButtonText = "跳转页数";
            this.ContainerTagName = "div";
            this.CssClass = "pagination"; //bootsrap 分页效果默认CSS类
            this.InvalidPageIndexErrorMessage = "页索引无效";
            this.PageIndexOutOfRangeErrorMessage = "页索引超出范围";
            this.MaxPageIndex = 0;
        }
    }
}
