using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;

namespace SrnprWeb.CommonFunction
{

    /// <summary>
    /// Author:Liudpc
    /// Create Date: 2010-11-10 16:09:18
    /// Description: 样式辅助类
    /// </summary>
    public class StyleHelperWCF
    {


        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-11-10 16:09:33
        /// Description: 根据样式字符串返回样式字典
        /// </summary>
        /// <param name="sStyle"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictByStyleString(string sStyle)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] str = sStyle.Trim().Split(';');
            foreach (string s in str)
            {
                string[] strStyle = s.Split(':');
                dict[strStyle[0]] = strStyle[1];
            }
            return dict;
        }


        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-11-10 16:09:46
        /// Description: 根据样式字符串返回样式中文描述
        /// </summary>
        /// <param name="sStyleString"></param>
        /// <param name="sFormat"></param>
        /// <param name="sJoin"></param>
        /// <returns></returns>
        public static string GetDescriptByStleString(string sStyleString,string sFormat,string sJoin)
        {
             string sReturn="";
            Dictionary<string, string> dict = GetDictByStyleString(sStyleString);
          

            List<string> listStyle = new List<string>();
            
            foreach (KeyValuePair<string, string> kvp in dict)
            {


                StyleDescriptionWWE sd = GetDescriptionEntity(kvp.Key);

                if (sd!=null)
                {
                    if (sd.Item != null)
                    {
                        listStyle.Add(string.Format(sFormat,sd.StyleName,sd.Item[kvp.Value]));

                    }
                    else
                    {
                        listStyle.Add(string.Format(sFormat, sd.StyleName, kvp.Value));
                    }
                }
            }
            sReturn = string.Join(sJoin, listStyle.ToArray());
            return sReturn;
        }




        public static StyleDescriptionWWE GetDescriptionEntity(string sStyleName)
        {
            StyleDescriptionWWE sd = new StyleDescriptionWWE();
            switch (sStyleName)
            {
                case "text-align":

                    sd.StyleName = sStyleName;
                    sd.StyleDescription = "对齐方式";
                    sd.Item = new Dictionary<string, string>();
                    sd.Item.Add("left", "左对齐");
                    sd.Item.Add("right", "右对齐");
                    sd.Item.Add("center", "居中");
                    break;
                case "width":
                    sd.StyleName = sStyleName;
                    sd.StyleDescription = "宽度";

                    break;

            }


            return sd;

        }





       



       





    }
}
