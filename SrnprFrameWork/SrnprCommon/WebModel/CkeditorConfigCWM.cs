using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.WebModel
{
    public class CkeditorConfigCWM:BaseEntity.WebWidgetConfigCBE
    {
        public override SrnprCommon.EnumCommon.WebWidgetTypeCEC WidgetType
        {
            get
            {
                return SrnprCommon.EnumCommon.WebWidgetTypeCEC.FileUploadWWW;
            }

        }
    }
}
