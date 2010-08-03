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
        /// Description: 显示类型
        /// Author:Liudpc
        /// Create Date: 2010-8-2 17:38:47
        /// </summary>
        public string ColumnType { get; set; }



        /// <summary>
        /// Description: 显示内容（最高优先级）
        /// Author:Liudpc
        /// Create Date: 2010-8-2 18:01:58
        /// </summary>
        public string ColumnShow { get; set; }


       







    }
}
