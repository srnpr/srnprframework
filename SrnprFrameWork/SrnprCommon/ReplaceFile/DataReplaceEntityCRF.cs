using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SrnprCommon.ReplaceFile
{




    /// <summary>
    /// Description: 数据库处理后结果
    /// Author:Liudpc
    /// Create Date: 2010-6-10 11:41:00
    /// </summary>
    public class DataReplaceEntityCRF
    {


        /// <summary>
        /// Description: 主替换参数集合
        /// Author:Liudpc
        /// Create Date: 2010-6-10 11:40:34
        /// </summary>
        public Dictionary<string, string> MainParms { get; set; }


        /// <summary>
        /// Description: 循环替换集合
        /// Author:Liudpc
        /// Create Date: 2010-6-10 11:40:45
        /// </summary>
        public List<DataTable> ListParms { get; set; }


    }
}
