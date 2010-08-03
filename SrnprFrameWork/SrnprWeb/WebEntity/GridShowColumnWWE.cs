using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowColumnWWE
    {


        public string Guid { get; set; }


        /// <summary>
        /// Description: 列头名称
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:23:37
        /// </summary>
        public string HeaderText { get; set; }



        /// <summary>
        /// Description: 数据列
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:36:19
        /// </summary>
        public string ColumnData { get; set; }


        /// <summary>
        /// Description: 显示类型（d:默认,r:单选框,c:复选框,l:超级链接,o:其他）
        /// Author:Liudpc
        /// Create Date: 2010-8-2 17:38:47
        /// </summary>
        public string ColumnType { get; set; }



        /// <summary>
        /// Description: 显示内容（最高优先级,此内容将覆盖掉前置设置）
        /// Author:Liudpc
        /// Create Date: 2010-8-2 18:01:58
        /// </summary>
        public string ColumnShow { get; set; }




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
