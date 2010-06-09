using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SrnprCommon.ReplaceFile
{
    public class ReplaceXmlCRF
    {


        /// <summary>
        /// 
        /// Description: 得到替换的内容
        /// Author:Liudpc
        /// Create Date: 2010-6-8 16:45:27
        /// </summary>
        /// <returns></returns>
        public string GetXmlReplaceContent()
        {




            return "";
        }





        public bool SendEmail()
        {

            return true;

        }



        public TempleteXmlEntityCRF GetTempleteXml(string sFilePath)
        {
            TempleteXmlEntityCRF txe = new TempleteXmlEntityCRF();

           
           


            return txe;
        }



        private TempleteCodeEntityCRF GetTempleteCode(string sFilePath)
        {
            TempleteCodeEntityCRF tce = new TempleteCodeEntityCRF();


            if (File.Exists(sFilePath))
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sFilePath);


            }
            else
            {
                AddLog("log20010015",sFilePath);
            }



            return tce;


        }





        private void AddLog(string sLogId, params string strParams)
        {



        }

    }
}
