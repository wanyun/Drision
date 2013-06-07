using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drision.MVCFrame.WebControls
{
    public interface IPagedList
    {
        int CurrentPageIndex { get; set; }

        int PageSize { get; set; }

        int TotalItemCount { get; set; }
    }
}
