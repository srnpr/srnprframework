using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;

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
        public static WebEntity.WidgetResponseWWE GetResponse(WebEntity.WidgetRequestWWE req, Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> dic)
        {


            


            WebEntity.WidgetResponseWWE res=new SrnprWeb.WebEntity.WidgetResponseWWE();
            res.RQ = new List<SrnprWeb.WebInterface.WidgetRequestWWI>();
            res.RS = new List<SrnprWeb.WebInterface.WidgetResponseWWI>();



            foreach (var t in req.RQ)
            {
                switch (t.WidgetType)
                {
                    case "LS":

                        res.RS.Add(LSW.GetResponse(t));
                        res.RQ.Add(t);

                        break;

                }


            }

            return res;


        }



        /// <summary>
        /// 
        /// Description: 返回输出信息
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:42:59
        /// </summary>
        /// <param name="sRequest"></param>
        /// <returns></returns>
        public static string Response(string sRequest)
        {
            return CommonFunction.JsonHelper.Serialize<WebEntity.WidgetResponseWWE>(GetResponse(CommonFunction.JsonHelper.Deserialize<WebEntity.WidgetRequestWWE>(sRequest),null));


        }




        /// <summary>
        /// 
        /// Description: 反序列化输入参数
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:42:41
        /// </summary>
        /// <param name="sRequest"></param>
        /// <returns></returns>
        public static WidgetRequestWWE DeserializeRequest(string sRequest)
        {
            return CommonFunction.JsonHelper.Deserialize<WebEntity.WidgetRequestWWE>(sRequest);
        }




        /// <summary>
        /// 
        /// Description: 返回输出信息
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:39:13
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string Response(WidgetRequestWWE req,Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> dic)
        {
            return CommonFunction.JsonHelper.Serialize<WebEntity.WidgetResponseWWE>(GetResponse(req,dic));


        }









    }
}
