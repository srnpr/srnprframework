using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class PageSerchConfigWWE:SrnprCommon.BaseEntity.WebWidgetConfigCBE
    {

        public override SrnprCommon.EnumCommon.WebWidgetTypeCEC WidgetType
        {
            get { return SrnprCommon.EnumCommon.WebWidgetTypeCEC.PageSerchWWW; }
        }
    }
}
