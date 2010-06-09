using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class DragFieldWWE
    {

        private long _dragElementId = 0;
        /// <summary>
        /// 元素编号
        /// </summary>
        public long DragElementId
        {
            get { return _dragElementId; }
            set { _dragElementId = value; }
        }

        private string _headerText = "";
        /// <summary>
        /// 表格抬头
        /// </summary>
        public string HeaderText
        {
            get { return _headerText; }
            set { _headerText = value; }
        }

      
    }
}
