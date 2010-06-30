using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SrnprCommon.CommonFunction
{

    public class XmlStaticCCF
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
            XmlNode xn = xnFather.SelectSingleNode(sNodeName);
            if (xn != null)
            {
                return xn.InnerText.Trim();

            }
            else
            {
                return null;
            }
        }


    }
}
