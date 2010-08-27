using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebInterface
{


    /// <summary>
    /// Description: 输出参数接口
    /// Author:Liudpc
    /// Create Date: 2010-8-27 10:33:39
    /// </summary>
    public interface WidgetResponseWWI
    {


        /// <summary>
        /// Description: 唯一编号
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:33:46
        /// </summary>
        string Guid { get; set; }


        /// <summary>
        /// Description: 类型
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:33:57
        /// </summary>
        string WidgetType { get; set; }
    }
}
