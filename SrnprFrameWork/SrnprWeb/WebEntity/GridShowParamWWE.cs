using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowParamWWE
    {

        public string Guid { get; set; }


        /// <summary>
        /// Description: 参数名称
        /// Author:Liudpc
        /// Create Date: 2010-8-2 15:07:35
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// Description: 参数对应的名称
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:42:42
        /// </summary>
        public string ColumnField { get; set; }




        /// <summary>
        /// Description: 参数操作符（d:默认自定义,e:等于,b:大于,s:小于,l:搜索匹配）
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:42:30
        /// </summary>
        public string ParamOperator { get; set; }



        /// <summary>
        /// Description: 查询类型（d:默认空,a:且,o:或）
        /// Author:Liudpc
        /// Create Date: 2010-8-4 15:14:54
        /// </summary>
        public string ParamQueryType { get; set; }




    }
}
