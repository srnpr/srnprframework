using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SrnprWeb.WebEntity
{


    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    public class GridShowColumnWWE:GridShowColumnBaseWWE
    {


       



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
        /// Description: 宽度描述
        /// Author:Liudpc
        /// Create Date: 2010-8-4 10:43:15
        /// </summary>
        public string Width { get; set; }




      



    }
}
