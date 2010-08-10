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
    public class GridShowRequestWWE
    {
      [DataMember(Order = 0)]

        /// <summary>
        /// Description: 编号
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:17:52
        /// </summary>
       
        public string Id { get; set; }

      [DataMember(Order =1)]
        /// <summary>
        /// Description: 当前页
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:06
        /// </summary>
        public long PageIndex { get; set; }

      [DataMember(Order = 2)]
        /// <summary>
        /// Description: 每页条数
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:20
        /// </summary>
        public long PageSize { get; set; }
      [DataMember(Order =3)]
        /// <summary>
        /// Description: 数量统计
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:30
        /// </summary>
        public long RowsCount { get; set; }

      [DataMember(Order =4)]
        /// <summary>
        /// Description: 客户端编号
        /// Author:Liudpc
        /// Create Date: 2010-8-2 16:56:10
        /// </summary>
        public string ClientId { get; set; }


      [DataMember(Order = 5)]
        /// <summary>
        /// Description: 处理类型 默认为空 由服务端处理 取值范围：client,server,demo
        /// Author:Liudpc
        /// Create Date: 2010-8-2 12:07:46
        /// </summary>
        public string ProcessType { get; set; }

      [DataMember(Order =6)]
        /// <summary>
        /// Description: 显示列
        /// Author:Liudpc
        /// Create Date: 2010-8-3 10:33:13
        /// </summary>
        public List<GridShowColumnBaseWWE> ShowColumn { get; set; }



     


      [DataMember(Order = 7)]

         /// <summary>
        /// Description: 查询内容
        /// Author:Liudpc
        /// Create Date: 2010-8-3 10:33:13
        /// </summary>
       public Dictionary<string, string> QueryDict { get; set; }


    }
}
