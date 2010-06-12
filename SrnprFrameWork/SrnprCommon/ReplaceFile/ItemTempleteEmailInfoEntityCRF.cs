using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class ItemTempleteEmailInfoEntityCRF:ItemTempleteEntityIfCRF
    {



        public  string Guid
        {
            get
          ;
            set;
        }

        public  SrnprCommon.EnumCommon.ItemTempleteType TempleteType
        {
            get { return SrnprCommon.EnumCommon.ItemTempleteType.EmailInfo; }
        }




        public string Title { get; set; }


        public string Content { get; set; }

    }
}
