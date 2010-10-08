using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SrnprCommon.CommonFunction
{

    /// <summary>
    /// Author:Liudpc
    /// Create Date: 2010-10-8 20:19:56
    /// Description: 实体相关操作类
    /// </summary>
    public class EntityStaticCCF
    {



        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-10-8 20:20:22
        /// Description: 字典转化为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static T DictToEntity<T>(T t,Dictionary<string,string> dict)
        {


            Type entityType = t.GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();


            for (int i = 0, j = entityProperties.Length; i < j; i++)
            {
                if ( dict.ContainsKey(entityProperties[i].Name) && entityProperties[i].CanWrite)
                {
                    entityProperties[i].SetValue(t, dict[entityProperties[i].Name], null);
                }


            }



            return t;
        }



        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-10-8 20:20:47
        /// Description: 实体转化为字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<string, string> EntityToDict<T>(T t)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Type entityType = t.GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            for (int i = 0, j = entityProperties.Length; i < j; i++)
            {
                dict[entityProperties[i].Name] = entityProperties[i].GetValue(t, null).ToString();
            }

            return dict;


        }




    }
}
