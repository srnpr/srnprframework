using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;
using System.Text.RegularExpressions;

namespace SrnprWeb.WebProcess
{


    /// <summary>
    /// Description: 控件处理基本类
    /// Author:Liudpc
    /// Create Date: 2010-8-23 17:53:35
    /// </summary>
    public class WidgetProcessWWP
    {
        private static ListShowWWP LSW = new ListShowWWP();

        private static GridShowWWP GSW = new GridShowWWP();

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



            for (int i=0,j= req.RQ.Count;i<j;i++)
            {
                switch (req.RQ[i].WidgetType)
                {
                    case "LS":

                        res.RS.Add(LSW.GetResponse(req.RQ[i],dic.ContainsKey(i)?dic[i]:null));
                        res.RQ.Add(req.RQ[i]);

                        break;

                    case "GS":
                        res.RS.Add(GSW.GetResponse(req.RQ[i], dic.ContainsKey(i) ? dic[i] : null));
                        res.RQ.Add(req.RQ[i]);
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




        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 16:07:06
        /// Description: 返回客户端执行函数
        /// </summary>
        /// <param name="FunctionName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string SwwJsBaseName(string FunctionName,params string[] param)
        {
            return "SWW."+FunctionName+"("+string.Join(",",param)+");";
        }



        #region  重新检测所有提交内容

        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 15:53:54
        /// Description: 重新生成提交内容
        /// </summary>
        /// <param name="sCont"></param>
        /// <returns></returns>
        public static string RecheckContent(string sCont)
        {

            //开始判断检测控件
            if (sCont.IndexOf("srnpr_srnpr_ck_control_type_id") > -1)
            {

                Regex re = new Regex("<img.*?srnpr_srnpr_ck_control_type_id.*?/>");

                MatchCollection mc = re.Matches(sCont);

                for (int i = 0, j = mc.Count; i < j; i++)
                {
                    Dictionary<string, string> dKvp = GetElementProp(mc[i].Value.ToString());

                    if (dKvp["srnpr_srnpr_ck_control_type_id"] == "gridshow")
                    {

                        if (dKvp.ContainsKey("src") && dKvp.ContainsKey("id"))
                        {
                            sCont = sCont.Replace(mc[i].Value, SrnprWeb.WebProcess.GridShowWWP.GetShowHtml(dKvp["srnpr_ck_gridshow_xmlid"], dKvp["id"]));
                        }
                    }
                    else if (dKvp["srnpr_srnpr_ck_control_type_id"] == "LS")
                    {
                        if (dKvp.ContainsKey("src") && dKvp.ContainsKey("id"))
                        {

                            Dictionary<string, string> dConfig = RecheckDic(dKvp, "WidgetType,Id,ShowType,PId,ShowDefault");

                            dConfig.Add("SId","SWW_LS_Span_"+dConfig["PId"]);

                            sCont = sCont.Replace(mc[i].Value, ListShowWWP.GetShowHtml(dConfig));

                        }
                    }
                    else if (dKvp["srnpr_srnpr_ck_control_type_id"] == "TD")
                    {
                        sCont = sCont.Replace(mc[i].Value, ToolDialogWWP.GetResponse(dKvp["id"], dKvp["tooldialogurl"]));
                    }


                }
            }
            return sCont;
        }



        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 16:27:49
        /// Description: 重新核查出需要的内容
        /// </summary>
        /// <param name="dOld"></param>
        /// <param name="sGetAttr"></param>
        /// <returns></returns>
        protected static Dictionary<string, string> RecheckDic(Dictionary<string, string> dOld,string sGetAttr)
        {

            Dictionary<string, string> dReturn = new Dictionary<string, string>();
            string[] str = sGetAttr.Split(',');

            foreach (string s in sGetAttr.Split(','))
            {
                if(dOld.ContainsKey("json_"+s.ToLower()))
                {
                    dReturn.Add(s, dOld["json_" + s.ToLower()]);
                }

            }
            return dReturn;

        }



        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-9-2 15:54:06
        /// Description: 得到元素属性
        /// </summary>
        /// <param name="sElm"></param>
        /// <returns></returns>
        protected static Dictionary<string, string> GetElementProp(string sElm)
        {

            Dictionary<string, string> dkvp = new Dictionary<string, string>();
            if (sElm.IndexOf(' ') > -1)
            {
                dkvp.Add("__srnpr_html_elment_name", sElm.Substring(1, sElm.IndexOf(' ')));
            }
            Regex re = new Regex(" .*?\\=\".*?\"");
            MatchCollection mc = re.Matches(sElm);

            for (int i = 0, j = mc.Count; i < j; i++)
            {
                dkvp[mc[i].Value.Substring(0, mc[i].Value.IndexOf('=')).ToString().ToLower().Trim()] = mc[i].Value.Substring(mc[i].Value.IndexOf('=') + 1).ToString().Trim().Trim('"');
            }
            return dkvp;
        }




        #endregion
    }
}
