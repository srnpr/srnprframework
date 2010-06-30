using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SrnprCommon.CommonFunction
{

    public static class XmlStaticCCF
    {

        /// <summary>
        /// 
        /// Description: 根据名称取出子元素并返回字符串  适用于可能存在
        /// Author:Liudpc
        /// Create Date: 2010-6-13 11:25:00
        /// </summary>
        /// <param name="xnFather"></param>
        /// <param name="sNodeName"></param>
        /// <returns></returns>
        public static string GetChildValueByName(XmlNode xnFather, string sNodeName)
        {
            
            return xnFather.SelectSingleNode(sNodeName) != null ? xnFather.SelectSingleNode(sNodeName).InnerText.Trim() : null;
        }



        /// <summary>
        /// 
        /// Description: 得到属性的值
        /// Author:Liudpc
        /// Create Date: 2010-6-30 13:21:29
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="sAttName"></param>
        /// <returns></returns>
        public static string GetAttValueByName(XmlNode xn, string sAttName)
        {
            return xn.Attributes[sAttName] != null ? xn.Attributes[sAttName].Value.Trim() : null;

        }


    }
}
