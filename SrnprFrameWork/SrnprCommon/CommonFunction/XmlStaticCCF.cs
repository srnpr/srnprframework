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



        /// <summary>
        /// 
        /// Description: 添加子元素
        /// Author:Liudpc
        /// Create Date: 2010-6-30 14:11:13
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="sNodeName"></param>
        /// <param name="sNodeInnerText"></param>
        /// <returns></returns>
        public static XmlNode AppendChildNode(XmlNode xn, string sNodeName, string sNodeInnerText)
        {
            XmlNode xnChild=xn.OwnerDocument.CreateElement(sNodeName);

            if (sNodeInnerText != null)
            {
                xnChild.InnerText = sNodeInnerText;
            }

            xn.AppendChild(xnChild);

            return xnChild;
        }


        /// <summary>
        /// 
        /// Description: 添加属性
        /// Author:Liudpc
        /// Create Date: 2010-6-30 16:15:25
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="sAttName"></param>
        /// <param name="sAttValue"></param>
        /// <returns></returns>
        public static bool AppendAtt(XmlNode xn, string sAttName, string sAttValue)
        {
            XmlAttribute xa = xn.OwnerDocument.CreateAttribute(sAttName);
            xa.Value = sAttValue;
            xn.Attributes.Append(xa);
            return true;
        }

    }
}
