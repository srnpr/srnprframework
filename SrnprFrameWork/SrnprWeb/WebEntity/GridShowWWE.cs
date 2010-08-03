using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowWWE
    {


        public string Id { get; set; }


        public string Guid { get; set; }


        public string Description { get; set; }



        /// <summary>
        /// Description: 数据信息
        /// Author:Liudpc
        /// Create Date: 2010-8-2 15:23:03
        /// </summary>
        public GridShowDataWWE TableInfo { get; set; }


        



        /// <summary>
        /// Description: 参数列表
        /// Author:Liudpc
        /// Create Date: 2010-8-3 9:12:08
        /// </summary>
        public List<GridShowParamWWE> ParamList
        {
            get;
            set;
        }


        /// <summary>
        /// Description: 显示内容列表
        /// Author:Liudpc
        /// Create Date: 2010-8-3 9:12:20
        /// </summary>
        public List<GridShowColumnWWE> ColumnList
        {
            get;
            set;
        }






    }
}
