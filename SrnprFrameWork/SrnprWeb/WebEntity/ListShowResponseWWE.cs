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


    /// <summary>
    /// 
    /// Description: 列表显示回应
    /// Author:Liudpc
    /// Create Date: 2010-8-24 10:10:20
    /// </summary>
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    [KnownType(typeof(ListShowResponseWWE))]
    public class ListShowResponseWWE:WidgetShowBaseWWE, WebInterface.WidgetResponseWWI
    {



        [DataMember(Order = 101)]
        public string HtmlString { get; set; }



        [DataMember(Order = 102)]
        public List<ItemKvdWWE> Kvd { get; set; }

        /// <summary>
        /// 
        /// Description: 类型
        /// Author:Liudpc
        /// Create Date: 2010-8-24 10:09:46
        /// </summary>
        [DataMember(Order = 101)]
        public string WidgetType
        {
            get { return "LS"; }
            set { }
        }

      
    }
}
