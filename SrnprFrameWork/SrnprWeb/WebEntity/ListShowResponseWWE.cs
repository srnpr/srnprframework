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
    public class ListShowResponseWWE:ListShowBaseWWE, WebInterface.WidgetResponseWWI
    {
      
    }
}
