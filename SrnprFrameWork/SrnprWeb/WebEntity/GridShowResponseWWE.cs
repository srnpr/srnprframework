using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace SrnprWeb.WebEntity
{
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    [KnownType(typeof(GridShowResponseWWE))]
    public class GridShowResponseWWE : WidgetShowBaseWWE, WebInterface.WidgetResponseWWI
    {
        [DataMember(Order = 100)]
        public GridShowRequestWWE Request { get; set; }


        [DataMember(Order = 101)]
        public string HtmlString { get; set; }



        [DataMember(Order = 103)]
        public List<List<string>> DataItem { get; set; }


        /// <summary>
        /// 
        /// Description: 类型
        /// Author:Liudpc
        /// Create Date: 2010-8-24 10:09:46
        /// </summary>
        [DataMember(Order = 102)]
        public string WidgetType
        {
            get { return "GS"; }
            set { }
        }


       
    }
}
