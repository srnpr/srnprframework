using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.WebModel
{
    public class FileUploadConfigCWM:BaseEntity.WebWidgetConfigCBE
    {



        public string UploadButtonText
        {
            get;
            set;
        }


        public override SrnprCommon.EnumCommon.WebWidgetTypeCEC WidgetType
        {
            get
            {
                return SrnprCommon.EnumCommon.WebWidgetTypeCEC.FileUploadWWW;
            }
           
        }


    }
}
