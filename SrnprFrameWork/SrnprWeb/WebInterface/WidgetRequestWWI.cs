using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;
using SrnprWeb.WebEntity;

namespace SrnprWeb.WebInterface
{
    

    /// <summary>
    /// Description: 输入参数接口
    /// Author:Liudpc
    /// Create Date: 2010-8-27 10:33:20
    /// </summary>
    public interface WidgetRequestWWI
    {


        /// <summary>
        /// Description: 编号
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:32:45
        /// </summary>
        string Id { get; set; }


        /// <summary>
        /// Description: 唯一编号 系统自动生成
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:32:57
        /// </summary>
         string Guid { get; set; }


        /// <summary>
        /// Description: 类型
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:33:06
        /// </summary>
         string WidgetType { get; set; }

    }
}
