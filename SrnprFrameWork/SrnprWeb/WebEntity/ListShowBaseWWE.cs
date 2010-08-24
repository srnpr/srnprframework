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
    /// Description: 列表显示基类
    /// Author:Liudpc
    /// Create Date: 2010-8-24 10:09:07
    /// </summary>
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    public class ListShowBaseWWE
    {

        /// <summary>
        /// 
        /// Description: 唯一编号
        /// Author:Liudpc
        /// Create Date: 2010-8-24 10:09:38
        /// </summary>
        [DataMember(Order = 0)]
        public string Guid
        {
            get;
            set;
        }

       

        /// <summary>
        /// 
        /// Description: 类型
        /// Author:Liudpc
        /// Create Date: 2010-8-24 10:09:46
        /// </summary>
        [DataMember(Order = 1)]
        public string WidgetType
        {
            get { return "LS"; }
            set { }
        }

       

    }
}
