using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Drision.MVCFrame.WebControls
{
    internal class PagerItem
    {
        [CompilerGenerated]
        private string _text;
        [CompilerGenerated]
        private int _pageIndex;
        [CompilerGenerated]
        private bool _disabled;
        [CompilerGenerated]
        private PagerItemType _type;

        internal string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }

        internal int PageIndex
        {
            get
            {
                return this._pageIndex;
            }
            set
            {
                this._pageIndex = value;
            }
        }

        internal bool Disabled
        {
            get
            {
                return this._disabled;
            }
            set
            {
                this._disabled = value;
            }
        }

        internal PagerItemType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public PagerItem(string text, int pageIndex, bool disabled, PagerItemType type)
        {

            this.Text = text;
            this.PageIndex = pageIndex;
            this.Disabled = disabled;
            this.Type = type;
        }
    }


}
