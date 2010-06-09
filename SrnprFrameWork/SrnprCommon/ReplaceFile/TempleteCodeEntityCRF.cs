using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{


    /// <summary>
    /// Description: 替换模板的代码模式类
    /// Author:Liudpc
    /// Create Date: 2010-6-8 17:47:18
    /// </summary>
    public class TempleteCodeEntityCRF
    {


        /// <summary>
        /// Description: 是否可用
        /// Author:Liudpc
        /// Create Date: 2010-6-8 17:47:44
        /// </summary>
        public bool Used { get; set; }




        /// <summary>
        /// Description: 描述信息
        /// Author:Liudpc
        /// Create Date: 2010-6-8 17:51:23
        /// </summary>
        public string Description { get; set; }



        /// <summary>
        /// Description: 参数
        /// Author:Liudpc
        /// Create Date: 2010-6-9 9:02:59
        /// </summary>
        public List<ItemPramEntityCRF> Parm { get; set; }

        /// <summary>
        /// Description: 主sql
        /// Author:Liudpc
        /// Create Date: 2010-6-9 9:03:14
        /// </summary>
        public List<ItemMainSqlEntityCRF> MainSql { get; set; }

        /// <summary>
        /// Description: 列表Sql
        /// Author:Liudpc
        /// Create Date: 2010-6-9 9:03:34
        /// </summary>
        public List<ItemListSqlEntityCRF> ListSql { get; set; }


        


    }
}
