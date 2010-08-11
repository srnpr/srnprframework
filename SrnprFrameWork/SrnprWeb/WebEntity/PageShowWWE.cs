using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{

    /// <summary>
    /// Description: 页面显示实体
    /// Author:Liudpc
    /// Create Date: 2010-8-11 13:28:40
    /// </summary>
    public class PageShowWWE
    {


        /// <summary>
        /// Description: 编号
        /// Author:Liudpc
        /// Create Date: 2010-8-11 13:28:36
        /// </summary>
        public string Id { get; set; }



        /// <summary>
        /// Description: 唯一编号
        /// Author:Liudpc
        /// Create Date: 2010-8-11 13:28:32
        /// </summary>
        public string Guid { get; set; }


        /// <summary>
        /// Description: 描述
        /// Author:Liudpc
        /// Create Date: 2010-8-11 13:28:50
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Description: 是否状态保存
        /// Author:Liudpc
        /// Create Date: 2010-8-11 13:29:18
        /// </summary>
        public bool ViewState { get; set; }


        /// <summary>
        /// Description: 模板编号
        /// Author:Liudpc
        /// Create Date: 2010-8-11 13:28:13
        /// </summary>
        public string TempId { get; set; }


        /// <summary>
        /// Description: 原始内容
        /// Author:Liudpc
        /// Create Date: 2010-8-9 11:24:04
        /// </summary>
        public string HtmlContent { get; set; }



        /// <summary>
        /// Description: 显示内容
        /// Author:Liudpc
        /// Create Date: 2010-8-9 12:04:08
        /// </summary>
        public string Content { get; set; }

        

    }
}
