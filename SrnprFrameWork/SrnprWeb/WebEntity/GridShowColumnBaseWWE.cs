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
    public class GridShowColumnBaseWWE
    {
        [DataMember(Order = 0)]
        public string Guid { get; set; }

       
        /// <summary>
        /// Description: 列头名称
        /// Author:Liudpc
        /// Create Date: 2010-7-29 16:23:37
        /// </summary>
        [DataMember(Order = 1)]
        public string HeaderText { get; set; }
        
        /// <summary>
        /// Description: 数据显示方式（d:默认,n:不显示,h:永久隐藏,）
        /// Author:Liudpc
        /// Create Date: 2010-8-3 9:24:30
        /// </summary>
        [DataMember(Order = 2)]
        public string ShowDisplay { get; set; }



        
        /// <summary>
        /// Description: 排序方式（d:默认,a:默认正序,e:默认倒序,n:不排序）
        /// Author:Liudpc
        /// Create Date: 2010-8-3 9:31:04
        /// </summary>
        [DataMember(Order = 3)]
        public string OrderType { get; set; }


        /// <summary>
        /// Excel导出类型  (d默认可导出  n不可导出)
        /// </summary>
        [DataMember(Order = 4)]
        public string ExcelType { get; set; }

    }
}
