using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowDataWWE
    {




        /// <summary>
        /// Description: 唯一主键
        /// Author:Liudpc
        /// Create Date: 2010-8-2 12:15:23
        /// </summary>
        public string KeyColumn { get; set; }


        /// <summary>
        /// Description: 数据库编号
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:34:14
        /// </summary>
        public string DataBaseId { get; set; }


        /// <summary>
        /// Description: 表名
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:46:48
        /// </summary>
        public string TableName { get; set; }



        /// <summary>
        /// Description: 是否排序
        /// Author:Liudpc
        /// Create Date: 2010-8-3 17:58:33
        /// </summary>
        public bool SortFlag { get; set; }


        /// <summary>
        /// Description: 条件字符串(默认条件 子输入参数和此并行)
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:46:21
        /// </summary>
        public string WhereString { get; set; }



    }
}
