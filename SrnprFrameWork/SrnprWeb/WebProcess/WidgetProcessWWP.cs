using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebProcess
{
    public class WidgetProcessWWP
    {



        public WebEntity.WidgetResponseWWE Response(WebEntity.WidgetRequestWWE req)
        {

            WebEntity.WidgetResponseWWE res=new SrnprWeb.WebEntity.WidgetResponseWWE();



            switch (req.WidgetType)
            {
                case SrnprWeb.EnumType.WidgetType.LS:

                    res.Request = new SrnprWeb.WebEntity.ListShowRequestWWE();

                    break;

            }







            return res;


        }








    }
}
