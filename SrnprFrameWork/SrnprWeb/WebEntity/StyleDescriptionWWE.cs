using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{

    /// <summary>
    /// Author:Liudpc
    /// Create Date: 2010-11-10 16:29:23
    /// Description: 样式描述类
    /// </summary>
    public class StyleDescriptionWWE
    {

        /// <summary>
        /// Author:Liudpc
        /// Create Date: 2010-11-10 16:29:23
        /// Description: 样式名称
        /// </summary>
        public string StyleName 
        { get; set; }
        /// <summary>
        /// Author:Liudpc
        /// Create Date: 2010-11-10 16:29:23
        /// Description: 样式描述
        /// </summary>
        public string StyleDescription { get; set; }

        /// <summary>
        /// Author:Liudpc
        /// Create Date: 2010-11-10 16:29:23
        /// Description: 样式子项
        /// </summary>
        public Dictionary<string, string> Item { get; set; }



    }
}
