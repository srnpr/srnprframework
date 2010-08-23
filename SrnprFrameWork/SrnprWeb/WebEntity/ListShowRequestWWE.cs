using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class ListShowRequestWWE:WebInterface.WidgetRequestWWI
    {

      







        #region WidgetRequestWWI 成员

        public string Guid
        {
            get;
            set;
        }

        #endregion

        #region WidgetRequestWWI 成员


        SrnprWeb.EnumType.WidgetType SrnprWeb.WebInterface.WidgetRequestWWI.WidgetType
        {
            get { return EnumType.WidgetType.LS; }
        }

        #endregion
    }
}
