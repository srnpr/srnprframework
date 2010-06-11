using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.CommonConfig
{
    public class ReplaceFileConfigEntityCCC
    {


        public List<ReplaceFile.ServerDatabaseEntityCRF> DataServerList { get; set; }


        public List<ReplaceFile.ServerEmailEntityCRF> EmailServerList { get; set; }


        public string SplitString { get; set; }


        public string ReplaceFrom { get; set; }

        public string MainParmReplace { get; set; }



    }
}
