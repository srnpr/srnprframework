using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprWeb.WebEntity;

namespace SrnprWeb.WebProcess
{
    public class ListShowWWP:WebInterface.WidgetProcessWWI
    {



       

        #region WidgetProcessWWI 成员

        public WebInterface.WidgetResponseWWI GetResponse(WebInterface.WidgetRequestWWI req)
        {
            ListShowResponseWWE res = new ListShowResponseWWE();







            return res;
        }


        #endregion


       

        
    }
}
