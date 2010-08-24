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
    [KnownType(typeof(ListShowResponseWWE))]
    public class WidgetResponseWWE
    {

       

        /// <summary>
        /// 
        /// Description: 返回内容
        /// Author:Liudpc
        /// Create Date: 2010-8-24 10:11:28
        /// </summary>
        [DataMember(Order = 0)]
        public List<WebInterface.WidgetResponseWWI> RS { get; set; }


        /// <summary>
        /// 
        /// Description: 提交请求
        /// Author:Liudpc
        /// Create Date: 2010-8-24 10:11:38
        /// </summary>
        [DataMember(Order = 1)]
        public List<WebInterface.WidgetRequestWWI> RQ { get; set; }

    }
}
