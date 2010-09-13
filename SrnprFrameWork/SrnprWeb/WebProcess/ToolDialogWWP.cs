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

            return "<span id=\"" + sSpanId + "\"></span>" + CommonFunction.JSHelper.CreateScriptDefer(WebProcess.WidgetProcessWWP.SwwJsBaseName("W.Dialog.ToolDialog", "{Id:\"" + sId + "\",url:\"" + sUrl + "\",SId:\"" + sSpanId + "\"}"));

           
        }




        #region WidgetProcessWWI 成员

        public WebInterface.WidgetResponseWWI GetResponse(WebInterface.WidgetRequestWWI req)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
