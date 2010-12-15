/******************************************************
 * Description: 基本通用全局配置类
 * Author: Liudpc
 * Create Date: 2010-2-8 11:31:35
 ******************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.WebModel
{
    


    /// <summary>
    /// Description: 通用配置类
    /// Author:Liudpc
    /// Create Date: 2010-2-8 11:34:56
    /// </summary>
   public class CommonWidgetConfigCWM : BaseEntity.WebWidgetConfigCBE
    {
        /// <summary>
        /// 配置类型
        /// </summary>
        public override SrnprCommon.EnumCommon.WebWidgetTypeCEC WidgetType
        {
            get
            {
                return SrnprCommon.EnumCommon.WebWidgetTypeCEC.CommonWidgetWWW;
            }

        }

        /// <summary>
        /// 配置默认版本
        /// </summary>
        public Version WidgetDefaultVersion
        {
            get;
            set;
        }


        public Dictionary<string, string> IncludeFileList
        {
            get;
            set;
        }


        /// <summary>
        /// XML配置文件路径
        /// </summary>
        public string XmlFilePath
        {
            get;
            set;
        }

       
    }
}
