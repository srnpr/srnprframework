using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.InterFace
{
    public interface WebWidgetBaseCIF
    {



        SrnprCommon.InterFace.WebWidgetConfigCIF WebWeightConfig { get; set; }

        string BuildVersion { get; set; }

        string RevisionVersion { get; set; }

    }
}
