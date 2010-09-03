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
    [KnownType(typeof(GridShowRequestWWE))]
    public class WidgetRequestWWE
    {

        [DataMember(Order = 0)]
        public List<WebInterface.WidgetRequestWWI> RQ { get; set; }



        [DataMember(Order = 1)]
        public bool ResultFlag { get; set; }


    }
}
