using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;

namespace SrnprWeb.EnumType
{
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    public enum WidgetType
    {

        LS,
        GS,
        PS
    }


}
