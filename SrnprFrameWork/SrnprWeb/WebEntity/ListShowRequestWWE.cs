﻿using System;
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
    public class ListShowRequestWWE:WidgetShowBaseWWE, WebInterface.WidgetRequestWWI
    {

        [DataMember(Order = 101)]
        public string MyString { get; set; }




        /// <summary>
        /// 
        /// Description: 显示类型
        /// Author:Liudpc
        /// Create Date: 2010-8-27 16:43:00
        /// </summary>
        [DataMember(Order = 102)]
        public string ShowType { get; set; }



        
    }
}
