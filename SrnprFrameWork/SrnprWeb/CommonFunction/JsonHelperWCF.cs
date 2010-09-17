using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization.Json;  

namespace SrnprWeb.CommonFunction
{
    public class JsonHelperWCF
    {

        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 14:29:38
        /// Description: 实体序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }


        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 14:29:52
        /// Description: 实体反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            T obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }



        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 14:34:09
        /// Description: 字典序列化
        /// </summary>
        /// <param name="dObj"></param>
        /// <returns></returns>
        public static string SerializeDic(Dictionary<string, string> dObj)
        {
            return "{"+string.Join(",", dObj.Select(t => t.Key + ":\"" + t.Value + "\"").ToArray())+"}";


        }


    }
}
