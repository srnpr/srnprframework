using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowColumnDictWWE
    {
/******************************************************
 * Author: Liudpc
 * Create Date: 2010-11-10 11:43:28
 * Description: 列类型
 ******************************************************/

        public static Dictionary<string, string> ColumnType
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("d", "默认");
                dict.Add("r", "单选框");
                dict.Add("c", "复选框");
                dict.Add("l", "超级链接");
                dict.Add("o", "其他");
                return dict;

            }
        }

        public static Dictionary<string, string> ShowDisplay
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("d", "默认");
                dict.Add("n", "不显示");
                dict.Add("h", "永久隐藏");
                dict.Add("e", "不显示但导出");
                return dict;

            }
        }


        public static Dictionary<string, string> OrderType
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("d", "默认");
                dict.Add("a", "默认正序");
                dict.Add("e", "默认倒序");
                dict.Add("n", "不排序");
                return dict;

            }
        }

       

    }
}
