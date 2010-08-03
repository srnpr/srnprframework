using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowColumnBaseWWE
    {

        public string Guid { get; set; }


        /// <summary>
        /// Description: 列头名称
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:23:37
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// Description: 数据显示方式（d:默认,n:不显示,h:永久隐藏）
        /// Author:Liudpc
        /// Create Date: 2010-8-3 9:24:30
        /// </summary>
        public string ShowDisplay { get; set; }



        /// <summary>
        /// Description: 排序方式（d:默认,a:默认正序,e:默认倒序,n:不排序）
        /// Author:Liudpc
        /// Create Date: 2010-8-3 9:31:04
        /// </summary>
        public string OrderType { get; set; }
    }
}
