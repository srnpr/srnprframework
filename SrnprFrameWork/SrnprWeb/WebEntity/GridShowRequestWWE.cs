using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowRequestWWE
    {

        /// <summary>
        /// Description: 编号
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:17:52
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// Description: 当前页
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:06
        /// </summary>
        public long PageIndex { get; set; }


        /// <summary>
        /// Description: 每页条数
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:20
        /// </summary>
        public long PageSize { get; set; }


        /// <summary>
        /// Description: 数量统计
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:30
        /// </summary>
        public long RowsCount { get; set; }




        /// <summary>
        /// Description: 处理类型 默认为空 由服务端处理 取值范围：client,server
        /// Author:Liudpc
        /// Create Date: 2010-8-2 12:07:46
        /// </summary>
        public string ProcessType { get; set; }


    }
}
