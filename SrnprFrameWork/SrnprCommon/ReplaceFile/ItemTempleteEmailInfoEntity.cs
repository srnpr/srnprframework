using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class ItemTempleteEmailInfoEntity:ItemTempleteEntityCRF
    {



        public override string Guid
        {
            get
          ;
            set;
        }

        public override SrnprCommon.EnumCommon.ItemTempleteType TempleteType
        {
            get { return SrnprCommon.EnumCommon.ItemTempleteType.EmailInfo; }
        }
    }
}
