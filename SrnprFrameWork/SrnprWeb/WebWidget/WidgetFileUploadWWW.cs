using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using SrnprCommon.EnumCommon;

namespace SrnprWeb.WebWidget
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WidgetFileUploadWWW runat=server></{0}:WidgetFileUploadWWW>")]
    public class WidgetFileUploadWWW : WebControl, SrnprCommon.InterFace.WebWidgetBaseCIF
    {

        #region 公共属性
        private string _uploadButtonText = "";
        /// <summary>
        /// 上传按钮文字
        /// </summary>
        public string UploadButtonText
        {
            get
            {
                if (string.IsNullOrEmpty(_uploadButtonText))
                {
                    return GetConfigDictionary(WebWidgetFileUploadWWWDictionary.UploadButtonText);
                }
                else
                {
                    return _uploadButtonText;
                }
            }
            set { _uploadButtonText = value; }
        }

        private string _resetButtonText;
        /// <summary>
        /// 重置按钮文字
        /// </summary>
        public string ResetButtonText
        {
            get { return _resetButtonText; }
            set { _resetButtonText = value; }
        }

        private bool _isAllowNull = false;
        /// <summary>
        /// 是否允许空 默认为不允许空
        /// </summary>
        public bool IsAllowNull
        {
            get
            {
                return _isAllowNull;
            }
            set
            {
                _isAllowNull = value;
            }
        }

        private string _allowPostFix = "";
        /// <summary>
        /// 允许上传文件类型 默认为
        /// .rar|.doc|.docx|.xls|.xlsx|.ppt|.pptx|.pdf|.jpg|.gif|.jpeg|.bmp|.txt|.rtf|.csv|.zip|.7z|
        /// </summary>
        public string AllowPostFix
        {
            get
            {
                if (string.IsNullOrEmpty(_allowPostFix))
                {
                    return GetConfigDictionary(SrnprCommon.EnumCommon.WebWidgetFileUploadWWWDictionary.AllowPostFix);
                }
                else
                {
                    return _allowPostFix;
                }
            }
            set
            {
                _allowPostFix = value;
            }
        }

        private string _webStyleWidth = "";
        /// <summary>
        /// 上传控件宽度
        /// </summary>
        public string WebStyleWidth
        {
            get
            {
                if (string.IsNullOrEmpty(_webStyleWidth))
                {
                    return GetConfigDictionary(WebWidgetFileUploadWWWDictionary.WebStyleWidth);
                }
                else
                {
                    return _webStyleWidth;
                }
            }
            set
            {
                _webStyleWidth = value;
            }
        }

        private string _buttonClass = "";
        /// <summary>
        /// 上传按钮样式
        /// </summary>
        public string ButtonClass
        {
            get
            {
                if (string.IsNullOrEmpty(_buttonClass))
                {
                    return GetConfigDictionary(WebWidgetFileUploadWWWDictionary.ButtonClass);
                }
                else
                {
                    return _buttonClass;
                }
            }
            set
            {
                _buttonClass = value;
            }
        }

        private int _saveFileType = 1;
        /// <summary>
        /// 保存文件类型  1为只上传到远程服务器（此类型下无返回FileServerFullPath）
        /// 2为远程服务器与web服务器都保存
        /// </summary>
        public int SaveFileType
        {
            get
            {
                return _saveFileType;
            }
            set
            {
                _saveFileType = value;
            }
        }


        private string _allowFileCount = "";
        /// <summary>
        /// 允许文件上传数量 如果设置了此参数则开始判断文件个数 此参数优先级低于IsAllowNull 
        /// eg:如果设置为2 则表示只允许上传2个文件   如果设置为2-4则表示最少2个最多4个文件
        ///    如果设置为-4则表示最多为4个文件  如果设置为2-则表示最少2个文件
        /// </summary>
        public string AllowFileCount
        {
            get { return _allowFileCount; }
            set { _allowFileCount = value; }
        }

        private string _uploadButtonEvalFunction = "";
        /// <summary>
        /// 设置上传按钮执行函数  默认情况下只执行上传提交功能 如果设置该参数则只执行该参数，
        /// 该参数请写为js函数
        /// </summary>
        public string UploadButtonEvalFunction
        {
            get { return _uploadButtonEvalFunction; }
            set { _uploadButtonEvalFunction = value; }
        }

        private string _maxFileLength = "";
        /// <summary>
        /// 最大上传文件大小(只支持MB,KB单位)
        /// eg:5MB  3KB  2000  3.5KB
        /// </summary>
        public string MaxFileLength
        {
            get
            {
                if (string.IsNullOrEmpty(_maxFileLength))
                {
                    return GetConfigDictionary(WebWidgetFileUploadWWWDictionary.MaxFileLength);
                }
                else
                {
                    return _maxFileLength;
                }
            }
            set
            {
                _maxFileLength = value;
            }
        }

        private string _fileType = "";
        /// <summary>
        /// 上传类型 暂时只支持参数为pic 图片类型  
        /// </summary>
        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }





        /// <summary>
        /// 是否有上传文件
        /// </summary>
        public bool IsHasFile
        {
            get
            {
                return _fileInfo.Count > 0;
            }

        }

        private List<WebEntity.FileUploadInfoWWE> _fileInfo;
        /// <summary>
        /// 上传文件实体List
        /// </summary>
        public List<WebEntity.FileUploadInfoWWE> FileInfo
        {
            get
            {




                return _fileInfo;
            }
            set
            {
                _fileInfo = value;



            }
        }

        private int _errorType = 0;
        /// <summary>
        /// 失败类型 0为正常 1为上传失败  2为文件大小超过限制
        /// </summary>
        public int ErrorType
        {
            get
            {
                return _errorType;
            }

        }


        #endregion


        #region 私有属性常量
        /// <summary>
        /// 上传编号
        /// </summary>
        private const string CONST_INPUT_FILE_ID_ADD = "_SRNPRWEB_WIDGETFILEUPLOADWWW_FILE";
        /// <summary>
        /// 按钮编号
        /// </summary>
        private const string CONST_INPUT_BUTTON_ID_ADD = "_SRNPRWEB_WIDGETFILEUPLOADWWW_BUTTON";
        /// <summary>
        /// 隐藏编号
        /// </summary>
        private const string CONST_INPUT_HIDDEN_ID_ADD = "_SRNPRWEB_WIDGETFILEUPLOADWWW_HIDDEN";
        /// <summary>
        /// 文件列表
        /// </summary>
        private const string CONST_SPAN_FILELIST_ID_ADD = "_SRNPRWEB_WIDGETFILEUPLOADWWW_SPAN";
        /// <summary>
        /// 文件类型
        /// </summary>
        private const string CONST_INPUT_TYPE_ID_ADD = "_SRNPRWEB_WIDGETFILEUPLOADWWW_TYPE";
        /// <summary>
        /// 分隔符
        /// </summary>
        private const string CONST_FILE_SPLIT_STRING = "####";
        #endregion


        #region 私有变量
        /// <summary>
        /// 文件字符串
        /// </summary>
        private string FileString = "";

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {


            base.OnInit(e);

            InitControl();


        }

        private void InitControl()
        {
            WidgetConfig = (SrnprCommon.WebModel.FileUploadConfigCWM)new SrnprCommon.WebFunction.LoadConfigCWF().GetWeightConfig(SrnprCommon.EnumCommon.WebWidgetTypeCEC.FileUploadWWW, BuildVersion, RevisionVersion);


            _fileInfo = new List<SrnprWeb.WebEntity.FileUploadInfoWWE>();


            if (HttpContext.Current != null && base.Page.Request[base.UniqueID + CONST_INPUT_HIDDEN_ID_ADD] != null)
            {
                FileString = base.Page.Request[base.UniqueID + CONST_INPUT_HIDDEN_ID_ADD].ToString().Trim();
                if (FileString == CONST_FILE_SPLIT_STRING)
                {
                    FileString = "";
                }

            }

            string[] strFile = Regex.Split(FileString, CONST_FILE_SPLIT_STRING, RegexOptions.IgnoreCase);
            for (int i = 1, j = strFile.Length; i < j; i = i + 2)
            {
                WebEntity.FileUploadInfoWWE fui = new SrnprWeb.WebEntity.FileUploadInfoWWE();
                fui.FileName = strFile[i - 1];
                fui.FileUrl = strFile[i];
                _fileInfo.Add(fui);
            }

            //判断是否有文件上传操作
            if (HttpContext.Current != null && base.Page.Request[base.UniqueID + CONST_INPUT_TYPE_ID_ADD] != null && base.Page.Request[base.UniqueID + CONST_INPUT_TYPE_ID_ADD].Trim() == "1")
            {
                HttpPostedFile hpf = base.Page.Request.Files.Get(base.UniqueID + CONST_INPUT_FILE_ID_ADD);

                _errorType = 1;

                if (hpf != null && hpf.ContentLength > 0)
                {

                    decimal dMax = -1;
                    dMax = decimal.Parse(MaxFileLength.ToUpper().Replace("MB", "").Replace("KB", ""));

                    if (MaxFileLength.ToUpper().IndexOf("MB") > -1)
                    {
                        dMax = dMax * 1024 * 1024;
                    }
                    else if (MaxFileLength.ToUpper().IndexOf("KB") > -1)
                    {
                        dMax = dMax * 1024;
                    }

                    if (hpf.ContentLength <= dMax)
                    {





                        WebEntity.FileUploadInfoWWE fui = new SrnprWeb.WebEntity.FileUploadInfoWWE();
                        fui.FileName = hpf.FileName.Substring(hpf.FileName.LastIndexOf('\\') + 1).Replace(CONST_FILE_SPLIT_STRING, "");
                        fui.FilePostFix = hpf.FileName.Substring(hpf.FileName.LastIndexOf('.')).ToLower();

                        if (fui.FilePostFix.Length > 2 && AllowPostFix.ToLower().IndexOf(fui.FilePostFix + "|") > -1)
                        {
                            string sNewFileName = Guid.NewGuid().ToString();


                            string sPathDir = "\\" + GetConfigDictionary(WebWidgetFileUploadWWWDictionary.UploadFilePathServer) + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

                            //开始尝试保存文件
                            try
                            {
                                if (SaveFileType == 2)
                                {
                                    string sDir = Page.Server.MapPath("~") + sPathDir;
                                    if (!Directory.Exists(sDir))
                                    {
                                        Directory.CreateDirectory(sDir);
                                    }

                                    fui.WebSiteServerFullPath = sDir + sNewFileName + fui.FilePostFix;
                                    hpf.SaveAs(fui.WebSiteServerFullPath);
                                    fui.FileUrl = sPathDir.Replace("\\", "/") + sNewFileName + fui.FilePostFix;
                                }

                                if (SaveFileType == 1 || SaveFileType == 2)
                                {
                                    byte[] bytes = new byte[hpf.InputStream.Length];
                                    hpf.InputStream.Read(bytes, 0, bytes.Length);
                                    hpf.InputStream.Seek(0, SeekOrigin.Begin);
                                    object returnObj = CommonFunction.GetWebServicesWCF.InvokeWebService(GetConfigDictionary(WebWidgetFileUploadWWWDictionary.FileSaveWebServicesUrl), "FileUpload", "SaveFile", new object[] { bytes, sPathDir, sNewFileName, 1, fui.FilePostFix });

                                    fui.FileUrl = returnObj.ToString().Trim().Split('|')[0];
                                }

                                FileString += (string.IsNullOrEmpty(FileString) ? "" : CONST_FILE_SPLIT_STRING) + fui.FileName + CONST_FILE_SPLIT_STRING + fui.FileUrl;

                                _fileInfo.Add(fui);


                                _errorType = 0;


                            }
                            catch
                            {

                            }

                        }

                    }
                    else
                    {
                        _errorType = 2;
                    }
                }
            }
        }



        /// <summary>
        /// 输出html
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {






            foreach (KeyValuePair<string, string> de in WidgetConfig.IncludeFile)
            {
                if (base.Page != null && !Page.ClientScript.IsClientScriptBlockRegistered(Page.GetType(), de.Key.ToString()))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), de.Key.ToString(), "", false);
                    writer.Write(de.Value.ToString());

                }
            }

            base.Render(writer);

            if (_fileInfo.Count > 0 && string.IsNullOrEmpty(FileString))
            {
                string[] str = new string[_fileInfo.Count * 2];
                for (int i = 0, j = _fileInfo.Count; i < j; i++)
                {
                    str[i * 2] = _fileInfo[i].FileName;
                    str[i * 2 + 1] = _fileInfo[i].FileUrl;
                }
                FileString = string.Join(CONST_FILE_SPLIT_STRING, str);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("<input type=\"file\" style=\"width:" + WebStyleWidth + "\" onkeydown=\"event.returnValue=false;\" onpaste=\"return false\" name=\"" + base.UniqueID + CONST_INPUT_FILE_ID_ADD + "\" id=\"" + base.UniqueID + CONST_INPUT_FILE_ID_ADD + "\"/>");
            sb.Append("<input type=\"button\" class=\"" + ButtonClass + "\" name=\"" + base.UniqueID + CONST_INPUT_BUTTON_ID_ADD + "\" id=\"" + base.UniqueID + CONST_INPUT_BUTTON_ID_ADD + "\" value=\"" + UploadButtonText + "\"/>");
            sb.Append("<input type=\"hidden\" name=\"" + base.UniqueID + CONST_INPUT_TYPE_ID_ADD + "\" id=\"" + base.UniqueID + CONST_INPUT_TYPE_ID_ADD + "\" value=\"\"/>");

            sb.Append("<input type=\"hidden\" name=\"" + base.UniqueID + CONST_INPUT_HIDDEN_ID_ADD + "\" id=\"" + base.UniqueID + CONST_INPUT_HIDDEN_ID_ADD + "\" value=\"" + FileString + "\"/>");
            sb.Append("<span id=\"" + base.UniqueID + CONST_SPAN_FILELIST_ID_ADD + "\"></span>");
            sb.Append("<script defer=\"defer\">SrnprWebWidgetFileUploadWWW({");

            if (IsAllowNull) sb.Append("isAllowNull:1,");
            if (!string.IsNullOrEmpty(_allowPostFix)) sb.Append("allowPostFix:\"" + _allowPostFix + "\",");
            if (!string.IsNullOrEmpty(_allowFileCount)) sb.Append("allowFileCount:\"" + _allowFileCount + "\",");
            if (!string.IsNullOrEmpty(_fileType)) sb.Append("fileType:\"" + _fileType + "\",");
            if (!string.IsNullOrEmpty(_uploadButtonEvalFunction)) sb.Append("uploadButtonEvalFunction:\"" + _uploadButtonEvalFunction + "\",");
            if (0 != _errorType)
                sb.Append("errorType:\"" + _errorType.ToString() + "\",");

            sb.Append("maxFileLength:\"" + MaxFileLength + "\",");
            sb.Append(" Id:\"" + base.UniqueID + "\",fId:\"" + CONST_INPUT_FILE_ID_ADD + "\",hId:\"" + CONST_INPUT_HIDDEN_ID_ADD + "\",tId:\"" + CONST_INPUT_TYPE_ID_ADD + "\",bId:\"" + CONST_INPUT_BUTTON_ID_ADD + "\",sId:\"" + CONST_SPAN_FILELIST_ID_ADD + "\",split:\"" + CONST_FILE_SPLIT_STRING + "\"})</script>");


            writer.Write(sb.ToString());
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //添加一个隐藏上传控件
            FileUpload fu = new FileUpload();
            fu.ID = Guid.NewGuid().ToString();
            this.Controls.Add(fu);
            fu.Style.Add(HtmlTextWriterStyle.Display, "none");
            /*
            Button b = new Button();
           
            b.ID = base.UniqueID + CONST_INPUT_BUTTON_ID_ADD;
            b.Text = UploadButtonText;
            //b.Click += new EventHandler(b_Click);
            base.Controls.Add(b);
            */



            /*
            HiddenField hf = new HiddenField();
            hf.ID = base.UniqueID + CONST_INPUT_HIDDEN_ID_ADD;
            hf.Value = FileString;

            base.Controls.Add(hf);
            */


        }

        /// <summary>
        /// 根据枚举得到配置内容
        /// </summary>
        /// <param name="d"></param>
        private string GetConfigDictionary(SrnprCommon.EnumCommon.WebWidgetFileUploadWWWDictionary d)
        {
            if (WidgetConfig.Dictionary.ContainsKey(d.ToString()))
            {
                return WidgetConfig.Dictionary[d.ToString()];
            }
            else
            {
                return "";
            }
        }




        public string GetHtml(string sAddUrl)
        {
            int iCount = FileInfo.Count;
            string[] strFile = new string[iCount];
            for (int i = 0; i < iCount; i++)
            {
                strFile[i] = "<a href=\"" + sAddUrl + FileInfo[i].FileUrl + "\" target=\"_blank\">" + FileInfo[i].FileName + "</a>";
            }

            return HttpUtility.HtmlEncode(string.Join("<br/>", strFile));
        }


        [Browsable(false)]
        public SrnprCommon.WebModel.FileUploadConfigCWM WidgetConfig
        {
            get;
            set;
        }





        #region WebWeightBaseIF 成员
        [Browsable(false)]
        public SrnprCommon.InterFace.WebWidgetConfigCIF WebWeightConfig
        {
            get
            {
                return WidgetConfig;
            }
            set
            {
                WidgetConfig = (SrnprCommon.WebModel.FileUploadConfigCWM)value;
            }
        }

        #endregion

        #region WebWeightBaseIF 成员



        public string BuildVersion
        {
            get
           ;
            set

           ;
        }

        #endregion

        #region WebWeightBaseIF 成员


        public string RevisionVersion
        {
            get;
            set;
        }

        #endregion
    }
}
