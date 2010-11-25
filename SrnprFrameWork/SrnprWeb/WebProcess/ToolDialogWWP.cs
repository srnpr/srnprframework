using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebProcess
{
    public class ToolDialogWWP : WebInterface.WidgetProcessWWI
    {









        public static string GetResponse(string sId, string sUrl)
        {

            string sSpanId = sId + "_span_" + Guid.NewGuid().ToString();

            return "<span id=\"" + sSpanId + "\"></span>" + CommonFunction.JSHelperWCF.CreateScriptDefer(WebProcess.WidgetProcessWWP.SwwJsBaseName("I", "{WidgetType:\"TD\",Id:\"" + sId + "\",url:\"" + sUrl + "\",SId:\"" + sSpanId + "\"}"));

           
        }

        public static string GetResponse(string sId, string sUrl,string sServerId,Dictionary<string,string> HiddenValue)
        {

            string sSpanId = sId + "_span_" + Guid.NewGuid().ToString();

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in HiddenValue)
            {
                sb.Append("<input type=\"hidden\" name=\"" + sId + "_Control_" + kvp.Key + "\" id=\"" + sId + "_" + kvp.Key + "\" value=\"" + kvp.Value + "\" />");
            }


            return "<span>"+sb.ToString().Trim()+"<span id=\"" + sSpanId + "\"></span></span>" + CommonFunction.JSHelperWCF.CreateScriptDefer(WebProcess.WidgetProcessWWP.SwwJsBaseName("I", "{WidgetType:\"TD\",Id:\"" + sId + "\",ServerId:\""+sServerId+"\",url:\"" + sUrl + "\",SId:\"" + sSpanId + "\"}"));


        }



        



        #region WidgetProcessWWI 成员

        public WebInterface.WidgetResponseWWI GetResponse(WebInterface.WidgetRequestWWI req)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
