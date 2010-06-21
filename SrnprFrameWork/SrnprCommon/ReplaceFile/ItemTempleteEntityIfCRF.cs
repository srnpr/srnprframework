using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{


    /// <summary>
    /// Description: 模板接口
    /// Author:Liudpc
    /// Create Date: 2010-6-21 15:24:13
    /// </summary>
    public interface ItemTempleteEntityIfCRF
    {


        /// <summary>
        /// Description: 唯一编号
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:23:55
        /// </summary>
        string Guid { get; set; }


        /// <summary>
        /// Description: 模板类型
        /// Author:Liudpc
        /// Create Date: 2010-6-21 15:24:04
        /// </summary>
        EnumCommon.ItemTempleteType TempleteType {get;}

    }
}
