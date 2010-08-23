using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class ListShowResponseWWE:WebInterface.WidgetResponseWWI
    {
        #region WidgetResponseWWI 成员

        public string Guid
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public SrnprWeb.EnumType.WidgetType WidgetType
        {
            get { return SrnprWeb.EnumType.WidgetType.LS; }
        }

        #endregion
    }
}
