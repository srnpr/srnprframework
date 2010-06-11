using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public abstract class ItemTempleteEntityAtCRF
    {

        public abstract string Guid { get; set; }

        public  abstract EnumCommon.ItemTempleteType TempleteType {get;}

    }
}
