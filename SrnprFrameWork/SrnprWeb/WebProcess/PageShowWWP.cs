﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SrnprWeb.WebEntity;

namespace SrnprWeb.WebProcess
{
    public class PageShowWWP
    {


        private static string sFilePath = "D:\\SrnprFrameWork\\WebWidget\\";
        private static string sFileExt = "PageShow\\{0}.www.ps.xml";

        private static string PageShowList = "PageShowList\\PageShowList.xml";


        public static void SaveFileByEntity(WebEntity.PageShowWWE psw)
        {
            SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.PageShowWWE>.EntityToXml(psw, sFilePath + string.Format(sFileExt, psw.Id));
        }


        public static WebEntity.PageShowWWE GetEntityById(string sId)
        {
            return SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.PageShowWWE>.XmlToEntity(sFilePath + string.Format(sFileExt, sId));
        }

        public static string GetShowHtml(WebEntity.PageShowWWE psw, string sId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(psw.Content);

            return sb.ToString();
        }


        public static WebEntity.PageShowListWWE GetList()
        {

            if (File.Exists(sFilePath + PageShowList))
            {
                return SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.PageShowListWWE>.XmlToEntity(sFilePath + PageShowList);
            }
            else
            {
                WebEntity.PageShowListWWE gsl = new PageShowListWWE();
                gsl.ItemList = new List<ItemBaseWWE>();
                return gsl;
            }

        }

        public static void SaveList(WebEntity.PageShowListWWE gsl)
        {
            SrnprCommon.CommonFunction.EntitySerializerCCF<WebEntity.PageShowListWWE>.EntityToXml(gsl, sFilePath + PageShowList);
        }




        public static string GetTempletesById(string sId)
        {

            return SrnprCommon.CommonFunction.IoStaticCCF.ReadFileContent(sFilePath + "\\CKTempletes\\"+sId+".www.ckt.html");
        }




        public static string RecheckContent(string sContent)
        {
            string str = sContent;

           




            return str;
        }


    }
}
