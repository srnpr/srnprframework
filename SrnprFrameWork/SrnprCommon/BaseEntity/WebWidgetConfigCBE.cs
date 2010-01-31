using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.BaseEntity
{

    /// <summary>
    /// 基类
    /// </summary>
    public  class WebWidgetConfigCBE:InterFace.WebWidgetConfigCIF
    {

        public Dictionary<string, string> IncludeFile
        {
            get;
            set;
        }
        public Dictionary<string, string> Dictionary
        {
            get;
            set;
        }



        #region WebWeightConfigIF 成员

        public Version DllVersion
        {
            get;
            set;
        }

        #endregion

        #region WebWeightConfigIF 成员


        public virtual SrnprCommon.EnumCommon.WebWidgetTypeCEC WidgetType
        {
            get;
            set;

        }

        #endregion
    }
}
