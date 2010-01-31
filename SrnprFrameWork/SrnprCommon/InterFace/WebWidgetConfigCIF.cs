using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.InterFace
{
    public interface WebWidgetConfigCIF
    {
        Version DllVersion { get; set; }
        EnumCommon.WebWidgetTypeCEC WidgetType { get;  }
    }
}
