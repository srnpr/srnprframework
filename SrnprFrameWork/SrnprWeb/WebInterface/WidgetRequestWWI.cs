using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;
using SrnprWeb.WebEntity;

namespace SrnprWeb.WebInterface
{
    
   
    public interface WidgetRequestWWI
    {


         string Guid { get; set; }


         string WidgetType { get; set; }

    }
}
