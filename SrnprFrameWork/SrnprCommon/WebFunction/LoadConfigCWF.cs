/******************************************************
 * Description: 加载配置
 * Author: Liudpc
 * Create Date: 2010-2-8 14:33:53
 ******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using System.Web;
using System.Web.Caching;

namespace SrnprCommon.WebFunction
{

    /// <summary>
    /// Description: 加载配置
    /// Author:Liudpc
    /// Create Date: 2010-2-8 14:34:06
    /// </summary>
    public class LoadConfigCWF
    {
        /// <summary>
        /// 定义配置值
        /// </summary>
        private const string SRNPR_WEB_CONFIG_PATH_NAME = "SrnprWebConfigPath";
        private const string SRNPR_WEB_CONFIG_CACHE_NAME = "SrnprCommon_WebFuntion_LoadConfigCWF_CaccheConfig";



        /// <summary>
        /// 
        /// Description: 得到控件配置
        /// Author:Liudpc
        /// Create Date: 2010-2-8 14:34:28
        /// </summary>
        /// <param name="widgetType"></param>
        /// <param name="sBuildVersion"></param>
        /// <param name="sRevisionVersion"></param>
        /// <returns></returns>
        public InterFace.WebWidgetConfigCIF GetWeightConfig(EnumCommon.WebWidgetTypeCEC widgetType, string sBuildVersion, string sRevisionVersion)
        {

            InterFace.WebWidgetConfigCIF widgetConfig;

            /*
            switch (widgetType)
            {
                case SrnprCommon.EnumCommon.WebWidgetTypeCEC.FileUploadWW:
                    WebModel.FileUploadConfigCWM fuc = new SrnprCommon.WebModel.FileUploadConfigCWM();
                    fuc.UploadButtonText = "testttttttttttttttt";
                    XmlDocument xd = new XmlDocument();
                    xd.Load(System.Configuration.ConfigurationManager.AppSettings[SRNPR_WEB_CONFIG_PATH_NAME]);
                    //xd.Load(@"S:\AAAProject\SrnprFrameWork\SrnprFile\ConfigFile\SrnprWebConfigXmlCF.xml");
                    fuc.UploadButtonText = xd.DocumentElement.ChildNodes.Count.ToString();

                    

                    
                    RegistryKey rstryKey = Registry.LocalMachine;
                    RegistryKey rgstryKeyValues = rstryKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion");

                    fuc.UploadButtonText = rgstryKeyValues.GetValue("ProductId").ToString();
                    rgstryKeyValues.Close();

                    fuc.UploadButtonText = System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString();
                    


                    widgetConfig = fuc;
                    break;
                default:
                    widgetConfig = new WebModel.FileUploadConfigCWM();
                    break;
            }
            */

            switch (widgetType)
            {
                case SrnprCommon.EnumCommon.WebWidgetTypeCEC.FileUploadWWW:
                    widgetConfig = new WebModel.FileUploadConfigCWM();
                    break;
                default:
                    widgetConfig = new WebModel.CommonWidgetConfigCWM();
                    break;
            }


            Dictionary<string, InterFace.WebWidgetConfigCIF> d ;

            if (System.Web.HttpContext.Current != null&&System.Web.HttpContext.Current.Cache[SRNPR_WEB_CONFIG_CACHE_NAME]!=null)
            {
                //从缓存读取配置
                d = (Dictionary<string, InterFace.WebWidgetConfigCIF>)System.Web.HttpContext.Current.Cache[SRNPR_WEB_CONFIG_CACHE_NAME];
            }
            else
            {
                //从xml读取配置
                d = GetListFromXML();
                if (System.Web.HttpContext.Current != null)
                {
                    //建立缓存依赖
                    System.Web.Caching.CacheDependency cd = new System.Web.Caching.CacheDependency(((WebModel.CommonWidgetConfigCWM)d[SrnprCommon.EnumCommon.WebWidgetTypeCEC.CommonWidgetWWW.ToString()]).XmlFilePath);
                    System.Web.HttpContext.Current.Cache.Insert(SRNPR_WEB_CONFIG_CACHE_NAME, d, cd, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
                }
            }

           


            widgetConfig = d[widgetType.ToString()];


            return widgetConfig;
        }


        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2009-11-25 11:53:46
        /// Description: 得到List
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, InterFace.WebWidgetConfigCIF> GetListFromXML()
        {
            Dictionary<string, InterFace.WebWidgetConfigCIF> dConfigReturn = new Dictionary<string, SrnprCommon.InterFace.WebWidgetConfigCIF>();
            WebModel.CommonWidgetConfigCWM commonConfig = new SrnprCommon.WebModel.CommonWidgetConfigCWM();
            Dictionary<string, string> dInculdeFile = new Dictionary<string, string>();
            XmlDocument xdXml = new XmlDocument();

            #region 开始读取xml文件路径 如果无法读取配置页面则读取注册表
            string sXmlPath = System.Configuration.ConfigurationManager.AppSettings[SRNPR_WEB_CONFIG_PATH_NAME];
            //如果无法读取配置文件  则开始读取注册表
            if (string.IsNullOrEmpty(sXmlPath))
            {
                string sVersion = System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString();
                RegistryKey rstryKey = Registry.LocalMachine;
                RegistryKey rgstryKeyValues = rstryKey.OpenSubKey(@"SOFTWARE\SrnprFrameWork\SrnprWeb\" + sVersion);
                if (rgstryKeyValues != null)
                {
                    object oPath = rgstryKeyValues.GetValue(SRNPR_WEB_CONFIG_PATH_NAME);
                    if (oPath != null)
                    {
                        sXmlPath = oPath.ToString();
                    }
                    rgstryKeyValues.Close();
                }

            }


            if (sXmlPath.IndexOf('~') == 0)
            {
                sXmlPath = System.Web.HttpContext.Current.Server.MapPath(sXmlPath);
            }

            //如果存在该路径 则开始加载XML
            if (string.IsNullOrEmpty(sXmlPath) || !System.IO.File.Exists(sXmlPath))
            {
                throw new Exception("Loading ConfigXML Error!!!");
            }

            #endregion



            xdXml.Load(sXmlPath);


            XmlNode xnDE = xdXml.DocumentElement;
            XmlNamespaceManager xmm = new XmlNamespaceManager(xdXml.NameTable);
            xmm.AddNamespace("s", xnDE.NamespaceURI);
            XmlNode xnCommon = xnDE.SelectSingleNode("s:commonConfig", xmm);





            #region 加载通用序列
            commonConfig.XmlFilePath = sXmlPath;
            commonConfig.DllVersion = new Version(xnCommon.SelectSingleNode("s:dllVersion", xmm).InnerText);
            commonConfig.WidgetDefaultVersion = new Version(xnCommon.SelectSingleNode("s:widgateDefaultVersion", xmm).InnerText);

            XmlNode xnIncludeList = xnCommon.NextSibling;


            XmlNodeList xnlInclude = xnIncludeList.ChildNodes;

            string sJsBaseUrl = xnIncludeList.Attributes["jsBaseUrl"].Value.Trim();
            string sCssBaseUrl = xnIncludeList.Attributes["cssBaseUrl"].Value.Trim();

            foreach (XmlNode xnFile in xnIncludeList.ChildNodes)
            {
                switch (xnFile.Name)
                {
                    case "js":

                        dInculdeFile.Add(xnFile.Attributes["fileId"].Value.Trim(), GetIncludeFileUrl(xnFile.InnerText, sJsBaseUrl, SrnprCommon.EnumCommon.WebWidgetIncludeFileType.JS));

                        break;
                    case "css":

                        dInculdeFile.Add(xnFile.Attributes["fileId"].Value.Trim(), GetIncludeFileUrl(xnFile.InnerText, sCssBaseUrl, SrnprCommon.EnumCommon.WebWidgetIncludeFileType.CSS));

                        break;
                }
            }

            commonConfig.IncludeFileList = dInculdeFile;
            dConfigReturn.Add(SrnprCommon.EnumCommon.WebWidgetTypeCEC.CommonWidgetWWW.ToString(), commonConfig);

            #endregion


            XmlNode xnWidgetConfig = xnDE.SelectSingleNode("s:widget[@version=" + commonConfig.WidgetDefaultVersion.Major.ToString() + "]/s:config[@version=" + commonConfig.WidgetDefaultVersion.Minor.ToString() + "]", xmm);

            #region 开始FileUpload
            XmlNodeList xnlFileUpload = xnWidgetConfig.SelectNodes("s:fileUpload", xmm);

            if (xnlFileUpload.Count > 0)
            {
                /*
                 List<string> listString = new List<string>();
                 foreach (XmlNode xnUpload in xnlFileUpload)
                 {
                     foreach(XmlNode xnVersion in xnUpload.SelectNodes("@version"))
                     {
                         if (listString.Contains(xnUpload.Attributes["version"].Value.Trim() + "." + xnVersion.Value.Trim()))
                         {
                             listString.Add(xnUpload.Attributes["version"].Value.Trim() + "." + xnVersion.Value.Trim());
                         }
                     }
                 }
                 */


                XmlNode xnlIncludeFile = xnlFileUpload[0].SelectSingleNode("s:includeFile", xmm);

                WebModel.FileUploadConfigCWM fuc = new SrnprCommon.WebModel.FileUploadConfigCWM();
                fuc.IncludeFile = new Dictionary<string, string>();
                foreach (XmlNode xnInc in xnlIncludeFile.ChildNodes)
                {
                    fuc.IncludeFile.Add(xnInc.InnerText.Trim(), dInculdeFile[xnInc.InnerText.Trim()]);

                }

                XmlNode xnlDictionary = xnlFileUpload[0].SelectSingleNode("s:dictionary", xmm);
                fuc.Dictionary = new Dictionary<string, string>();
                foreach (XmlNode xnDic in xnlDictionary.ChildNodes)
                {
                    fuc.Dictionary.Add(xnDic.Attributes["key"].Value.ToString(), xnDic.Attributes["value"].Value.ToString().Trim());

                }

                dConfigReturn.Add(SrnprCommon.EnumCommon.WebWidgetTypeCEC.FileUploadWWW.ToString(), fuc);
            }



            #endregion


            #region xmlreader版本注释
            /*
            XmlReader xr = XmlReader.Create(sXmlPath);
            WebModel.CommonWidgetCOnfigWM commonConfig = new SrnprCommon.WebModel.CommonWidgetCOnfigWM();

            //读取DLL版本
            if(xr.ReadToFollowing("dllVersion"))
            commonConfig.DllVersion =new Version(xr.ReadString()) ;

            //读取Widget默认版本
            if (xr.ReadToFollowing("widgateDefaultVersion"))
                commonConfig.WidgetDefaultVersion =new Version( xr.ReadString());

            listReturn.Add(EnumCommon.WebWidgetTypeEC.CommonWidgetWW.ToString(), commonConfig);


            Dictionary<string, string> dIncludeFile = new Dictionary<string, string>();

            if (xr.ReadToFollowing("includeFileList"))
            {
                xr.MoveToAttribute("jsBaseUrl");
                string sJsBaseUrl = xr.Value;
                xr.MoveToElement();
                xr.MoveToAttribute("cssBaseUrl");
                string sCssBaseUrl = xr.Value;
                xr.MoveToElement();

                while (xr.Read())
                {
                    
                    if (xr.Name != "widgetList")
                    {
                        switch (xr.Name)
                        {
                            case "js":
                                if (xr.MoveToAttribute("fileId"))
                                {
                                    string sFileId = xr.Value;
                                    xr.MoveToElement();

                                    dIncludeFile.Add(sFileId, GetIncludeFileUrl(xr.ReadString(), sJsBaseUrl,SrnprCommon.EnumCommon.WebWidgetIncludeFileType.JS));
                                }
                                break;
                            case "css":
                                if (xr.MoveToAttribute("fileId"))
                                {
                                    string sFileId = xr.Value;
                                    xr.MoveToElement();
                                    dIncludeFile.Add(sFileId, GetIncludeFileUrl(xr.ReadString(), sCssBaseUrl,SrnprCommon.EnumCommon.WebWidgetIncludeFileType.CSS));
                                }
                                break;
                        }
                    }
                    else
                    {
                        xr.Skip();
                        break;
                    }
                }
            }

            if (xr.ReadToFollowing("widget") && xr.MoveToAttribute("version") && xr.Value == commonConfig.WidgetDefaultVersion.Major.ToString())
            {
                string sMajor = xr.Value;
                if (xr.ReadToFollowing("config") && xr.MoveToAttribute("version") && xr.Value == commonConfig.WidgetDefaultVersion.Minor.ToString())
                {
                    string sMinor = xr.Value;

                    while (xr.Read())
                    {
                        switch (xr.Name)
                        {
                            case "pageSerch":

                                if (xr.ReadToFollowing("includeFile"))
                                {
                                    if (xr.ReadToDescendant("file"))
                                    {
                                    List<string> listInclude = new List<string>();
                                    listInclude.Add(dIncludeFile[xr.ReadString()]);

                                        
                                    }
                                }

                                break;
                            default:
                                xr.Skip();
                                break;
                        }


                    }

                }
            }



            xr.Close();
             * 
             * 
             */
            #endregion

            return dConfigReturn;
        }



        private string GetIncludeFileUrl(string sPath, string sBaseUrl, EnumCommon.WebWidgetIncludeFileType fileType)
        {
            string sReturn = "";
            if (StaticFunctionCWF.IsHttpUrl(sPath))
            {
                sReturn = sPath;
            }
            else
            {
                sReturn = sBaseUrl + sPath;
            }

            switch (fileType)
            {
                case SrnprCommon.EnumCommon.WebWidgetIncludeFileType.JS:
                    sReturn = string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", sReturn);
                    break;
                case SrnprCommon.EnumCommon.WebWidgetIncludeFileType.CSS:
                    sReturn = string.Format("<link rel=\"Stylesheet\" href=\"{0}\" />", sReturn);
                    break;
            }


            return sReturn;
        }







    }
}
