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
    [KnownType(typeof(GridShowRequestWWE))]
    public class GridShowRequestWWE : WidgetShowBaseWWE, WebInterface.WidgetRequestWWI
    {


        /// <summary>
        /// 
        /// Description: 类型
        /// Author:Liudpc
        /// Create Date: 2010-8-24 10:09:46
        /// </summary>
        [DataMember(Order = 101)]
        public string WidgetType
        {
            get { return "GS"; }
            set { }
        }


      [DataMember(Order =101)]
        /// <summary>
        /// Description: 当前页
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:06
        /// </summary>
        public long PageIndex { get; set; }

      [DataMember(Order =102)]
        /// <summary>
        /// Description: 每页条数
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:20
        /// </summary>
        public long PageSize { get; set; }
      [DataMember(Order =103)]
        /// <summary>
        /// Description: 数量统计  为-1时表示重新查询  为-2时表示分组更改标识
        /// Author:Liudpc
        /// Create Date: 2010-8-2 11:18:30
        /// </summary>
        public long RowsCount { get; set; }

      [DataMember(Order =104)]
        /// <summary>
        /// Description: 客户端编号
        /// Author:Liudpc
        /// Create Date: 2010-8-2 16:56:10
        /// </summary>
        public string ClientId { get; set; }


      [DataMember(Order =105)]
        /// <summary>
        /// Description: 处理类型 默认为空 由服务端处理 取值范围：client,server,demo
        /// Author:Liudpc
        /// Create Date: 2010-8-2 12:07:46
        /// </summary>
        public string ProcessType { get; set; }

      [DataMember(Order =106)]
        /// <summary>
        /// Description: 显示列
        /// Author:Liudpc
        /// Create Date: 2010-8-3 10:33:13
        /// </summary>
        public List<GridShowColumnBaseWWE> ShowColumn { get; set; }



     


      [DataMember(Order =107)]

         /// <summary>
        /// Description: 查询内容
        /// Author:Liudpc
        /// Create Date: 2010-8-3 10:33:13
        /// </summary>
       public Dictionary<string, string> QueryDict { get; set; }

      [DataMember(Order =108)]
      /// <summary>
      /// Description: 分组字典
      /// Author:Liudpc
      /// Create Date: 2010-8-3 10:33:13
      /// </summary>
      public List<ItemKvdWWE> GroupKvd { get; set; }

      [DataMember(Order =109)]
        /// <summary>
        /// Description: 分组值
        /// Author:Liudpc
        /// Create Date: 2010-8-12 17:52:22
        /// </summary>
      public string GroupValue { get; set; }



    
    }
}
