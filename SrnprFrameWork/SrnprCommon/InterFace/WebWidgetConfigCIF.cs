/******************************************************
 * Description: Web控件配置接口
 * Author: Liudpc
 * Create Date: 2010-6-7 17:05:24
 ******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.InterFace
{
    /// <summary>
    /// Description: Web控件配置接口
    /// Author: Liudpc
    /// Create Date: 2010-6-7 17:05:24
    /// </summary>
    public interface WebWidgetConfigCIF
    {
        /// <summary>
        ///  Description: 控件版本
        ///  Author: Liudpc
        ///  Create Date: 2010-6-7 17:05:54
        /// </summary>
        Version DllVersion { get; set; }

        /// <summary>
        ///  Description: 控件类型
        ///  Author: Liudpc
        ///  Create Date: 2010-6-7 17:05:54
        /// </summary>
        EnumCommon.WebWidgetTypeCEC WidgetType { get;  }
    }
}
