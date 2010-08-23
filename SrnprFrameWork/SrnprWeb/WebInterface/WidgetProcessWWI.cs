using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebInterface
{


    /// <summary>
    /// Description: 控件处理接口
    /// Author:Liudpc
    /// Create Date: 2010-8-23 17:53:02
    /// </summary>
    public interface WidgetProcessWWI
    {


        /// <summary>
        /// 
        /// Description: 得到返回内容
        /// Author:Liudpc
        /// Create Date: 2010-8-23 17:53:11
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        WebInterface.WidgetResponseWWI GetResponse(WebInterface.WidgetRequestWWI req);


    }
}
