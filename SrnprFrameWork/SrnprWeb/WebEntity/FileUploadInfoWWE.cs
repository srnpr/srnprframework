using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class FileUploadInfoWWE
    {
        private string _fileName = "";

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        private string _fileUrl = "";

        public string FileUrl
        {
            get
            {
                return _fileUrl;
            }
            set
            {
                _fileUrl = value;
            }
        }

        private string _webSiteServerFullPath = "";
        /// <summary>
        /// 网站服务器文件路径(如果是WidgetFileUploadWWW调用的SaveFileType设置为1时无返回值)
        /// </summary>
        public string WebSiteServerFullPath
        {
            get
            {
                return _webSiteServerFullPath;
            }
            set
            {
                _webSiteServerFullPath = value;
            }
        }



        private string _filePostFix = "";

        public string FilePostFix
        {
            get
            {
                return _filePostFix;
            }
            set
            {
                _filePostFix = value;
            }
        }


    }
}
