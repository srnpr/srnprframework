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
        /// Description: 参数操作符
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:42:30
        /// </summary>
        public string ParamOperator { get; set; }




    }
}
