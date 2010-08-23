using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class WidgetResponseWWE
    {


        public EnumType.WidgetType WidgetType { get; set; }


        public WebInterface.WidgetResponseWWI Response { get; set; }


        public WebInterface.WidgetRequestWWI Request { get; set; }

    }
}
