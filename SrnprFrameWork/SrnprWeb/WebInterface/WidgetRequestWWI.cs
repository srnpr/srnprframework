﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebInterface
{
    public interface WidgetRequestWWI
    {


         string Guid { get; set; }


         EnumType.WidgetType WidgetType { get;  }

    }
}