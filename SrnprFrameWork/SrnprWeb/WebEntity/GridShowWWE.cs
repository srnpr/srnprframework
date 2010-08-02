using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowWWE
    {


        public string Guid { get; set; }




        /// <summary>
        /// Description: 数据信息
        /// Author:Liudpc
        /// Create Date: 2010-8-2 15:23:03
        /// </summary>
        public GridShowDataWWE TableInfo { get; set; }


        



        public List<GridShowParamWWE> ParamList
        {
            get;
            set;
        }


        public List<GridShowColumnWWE> ColumnList
        {
            get;
            set;
        }






    }
}
