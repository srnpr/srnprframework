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
    
    public class ListShowRequestWWE:WebInterface.WidgetRequestWWI
    {

      







        #region WidgetRequestWWI 成员
        [DataMember(Order = 0)]
        
        public string Guid
        {
            get;
            set;
        }

        #endregion



        #region WidgetRequestWWI 成员
        
        [DataMember(Order = 1)]
        public string WidgetType
        {
            get { return "LS"; }
            set { }
        }

        #endregion
    }
}
