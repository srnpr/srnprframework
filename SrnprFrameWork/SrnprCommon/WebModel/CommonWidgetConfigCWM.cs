using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.WebModel
{
    class CommonWidgetConfigCWM : BaseEntity.WebWidgetConfigCBE
    {
        public override SrnprCommon.EnumCommon.WebWidgetTypeCEC WidgetType
        {
            get
            {
                return SrnprCommon.EnumCommon.WebWidgetTypeCEC.CommonWidgetWWW;
            }

        }


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

       
    }
}
