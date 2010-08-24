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
    /// Description: 列表显示请求
    /// Author:Liudpc
    /// Create Date: 2010-8-24 10:10:05
    /// </summary>
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    [KnownType(typeof(ListShowRequestWWE))]
    public class ListShowRequestWWE:ListShowBaseWWE, WebInterface.WidgetRequestWWI
    {

        [DataMember(Order = 101)]
        public string MyString { get; set; }






        
    }
}
