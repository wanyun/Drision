using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drision.MVCFrame.WebControls
{
    public static class PageLinqExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            int count = (pageIndex - 1) * pageSize;
            IQueryable<T> queryable = Queryable.Take<T>(Queryable.Skip<T>(allItems, count), pageSize);
            int totalItemCount = Queryable.Count<T>(allItems);
            return new PagedList<T>((IEnumerable<T>)queryable, pageIndex, pageSize, totalItemCount);
        }
    }
}
