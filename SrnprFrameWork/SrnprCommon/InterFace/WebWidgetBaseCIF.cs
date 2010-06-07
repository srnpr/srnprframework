/******************************************************
 * Description: Web控件基类接口
 * Author: Liudpc
 * Create Date: 2010-6-7 17:03:49
 ******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.InterFace
{
    ///  Description: Web控件接口函数
    ///  Author: Liudpc
    ///  Create Date: 2010-6-7 17:04:19
    /// </summary>
    public interface WebWidgetBaseCIF
    {



        SrnprCommon.InterFace.WebWidgetConfigCIF WebWeightConfig { get; set; }

        string BuildVersion { get; set; }

        string RevisionVersion { get; set; }

    }
}
