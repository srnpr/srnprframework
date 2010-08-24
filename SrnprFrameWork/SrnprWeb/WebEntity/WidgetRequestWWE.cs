using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;

namespace SrnprWeb.WebEntity
{
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    [KnownType(typeof(ListShowRequestWWE))]
    public class WidgetRequestWWE
    {

        [DataMember(Order = 0)]
        public EnumType.WidgetType WidgetType { get; set; }

        [DataMember(Order = 1)]
        public WebInterface.WidgetRequestWWI Request { get; set; }



    }
}
