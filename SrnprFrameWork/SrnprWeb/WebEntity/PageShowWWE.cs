using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class PageShowWWE
    {

        public string Id { get; set; }

        public string Guid { get; set; }

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
