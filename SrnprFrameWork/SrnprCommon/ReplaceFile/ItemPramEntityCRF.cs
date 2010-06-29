using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{


    /// <summary>
    /// Description: 输入参数实体
    /// Author:Liudpc
    /// Create Date: 2010-6-8 17:52:42
    /// </summary>
    public class ItemPramEntityCRF
    {

        /// <summary>
        /// Description: 参数名称
        /// Author:Liudpc
        /// Create Date: 2010-6-8 17:53:08
        /// </summary>
        public string ParmName { get; set; }



        /// <summary>
        /// Description: 参数的模板替换标识
        /// Author:Liudpc
        /// Create Date: 2010-6-8 17:53:39
        /// </summary>
        public string ParmText { get; set; }



        /// <summary>
        /// Description: 参数的唯一标识
        /// Author:Liudpc
        /// Create Date: 2010-6-29 15:14:24
        /// </summary>
        public string Guid { get; set; }

    }
}
