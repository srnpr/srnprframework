using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.EnumCommon
{
    /// <summary>
    /// 控件类型枚举
    /// </summary>
    public enum WebWidgetTypeCEC
    {
        /// <summary>
        /// 通用配置专用
        /// </summary>
        CommonWidgetWWW,
        /// <summary>
        /// 查询
        /// </summary>
        PageSerchWWW,
        /// <summary>
        /// 上传
        /// </summary>
        FileUploadWWW,
        /// <summary>
        /// 文本编辑器
        /// </summary>
        CkeditorWWW
    }

    public enum WebWidgetFileUploadWWWDictionary
    {
        /// <summary>
        /// 上传文件保存地址
        /// </summary>
        UploadFilePathServer,
        /// <summary>
        /// 上传按钮文字
        /// </summary>
        UploadButtonText,
        /// <summary>
        /// 允许上传文件类型后缀
        /// </summary>
        AllowPostFix,
        /// <summary>
        /// 上传按钮样式
        /// </summary>
        ButtonClass,
        /// <summary>
        /// 上传控件宽度
        /// </summary>
        WebStyleWidth,
        /// <summary>
        /// 文件上传WebServices路径
        /// </summary>
        FileSaveWebServicesUrl,
        
        /// <summary>
        /// 最大上传文件大小
        /// </summary>
        MaxFileLength

    }

    public enum WebWidgetIncludeFileType
    {
        /// <summary>
        /// Javascript
        /// </summary>
        JS,
        /// <summary>
        /// CSS
        /// </summary>
        CSS
    }







 


}
