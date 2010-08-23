using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebProcess
{
    public class ListShowWWP
    {


        public WebInterface.WidgetRequestWWI GetRequest()
        {
            var t= new WebEntity.ListShowRequestWWE();
            t.Guid = "";

            return t;
        }


    }
}
