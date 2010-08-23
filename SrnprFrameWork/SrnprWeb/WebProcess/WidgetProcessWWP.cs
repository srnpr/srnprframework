using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebProcess
{


    /// <summary>
    /// Description: 控件处理基类
    /// Author:Liudpc
    /// Create Date: 2010-8-23 17:53:35
    /// </summary>
    public class WidgetProcessWWP
    {
        private static ListShowWWP LSW = new ListShowWWP();



        /// <summary>
        /// 
        /// Description: 得到返回内容
        /// Author:Liudpc
        /// Create Date: 2010-8-23 17:53:55
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static WebEntity.WidgetResponseWWE GetResponse(WebEntity.WidgetRequestWWE req)
        {

            WebEntity.WidgetResponseWWE res=new SrnprWeb.WebEntity.WidgetResponseWWE();



            switch (req.WidgetType)
            {
                case SrnprWeb.EnumType.WidgetType.LS:

                    res.Response = LSW.GetResponse(req.Request);

                    break;
                default:
                    res.Response = null;
                    break;

            }


            res.Request = req.Request;

            if (res.Response != null)
            {
                res.WidgetType = res.Response.WidgetType;
            }


            return res;


        }



        public static string Response(string sRequest)
        {
            return CommonFunction.JsonHelper.Serialize<WebEntity.WidgetResponseWWE>(GetResponse(CommonFunction.JsonHelper.Deserialize<WebEntity.WidgetRequestWWE>(sRequest)));


        }









    }
}
