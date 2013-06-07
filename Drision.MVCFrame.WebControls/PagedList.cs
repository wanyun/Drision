using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Drision.MVCFrame.WebControls
{
    public enum PageIndexBoxType
    {
        TextBox,
        DropDownList,
    }

    internal enum PagerItemType : byte
    {
        FirstPage,
        NextPage,
        PrevPage,
        LastPage,
        MorePage,
        NumericPage,
    }

    public class PagedList<T> : List<T>, IPagedList
    {
        [CompilerGenerated]
        private int _currentPageIndex;
        [CompilerGenerated]
        private int _pageSize;
        [CompilerGenerated]
        private int _totalItemCount;

        public int CurrentPageIndex
        {
            get
            {
                return this._currentPageIndex;
            }
            set
            {
                this._currentPageIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                this._pageSize = value;
            }
        }

        public int TotalItemCount
        {
            get
            {
                return this._totalItemCount;
            }
            set
            {
                this._totalItemCount = value;
            }
        }

        public int TotalPageCount
        {
            get
            {
                return (int)Math.Ceiling((double)this.TotalItemCount / (double)this.PageSize);
            }
        }

        public int StartRecordIndex
        {
            get
            {
                return (this.CurrentPageIndex - 1) * this.PageSize + 1;
            }
        }

        public int EndRecordIndex
        {
            get
            {
                if (this.TotalItemCount <= this.CurrentPageIndex * this.PageSize)
                    return this.TotalItemCount;
                else
                    return this.CurrentPageIndex * this.PageSize;
            }
        }

        public PagedList(IList<T> items, int pageIndex, int pageSize)
        {

            this.PageSize = pageSize;
            this.TotalItemCount = items.Count;
            this.CurrentPageIndex = pageIndex;
            for (int index = this.StartRecordIndex - 1; index < this.EndRecordIndex; ++index)
                this.Add(items[index]);
        }

        public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {

            this.AddRange(items);
            this.TotalItemCount = totalItemCount;
            this.CurrentPageIndex = pageIndex;
            this.PageSize = pageSize;
        }
    }
}
