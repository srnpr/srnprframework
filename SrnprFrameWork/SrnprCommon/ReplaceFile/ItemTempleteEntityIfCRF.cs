using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public interface ItemTempleteEntityIfCRF
    {

        string Guid { get; set; }

        EnumCommon.ItemTempleteType TempleteType {get;}

    }
}
