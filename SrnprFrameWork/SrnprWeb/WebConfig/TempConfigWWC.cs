using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebConfig
{
    public class TempConfigWWC
    {


        public WebConfig.ConfigEntityWWC ConfigTest()
        {

            WebConfig.ConfigEntityWWC ce = new ConfigEntityWWC();

            ce.DataServer = new List<SrnprCommon.CommonConfig.DataServerEntityCCC>();
            ce.DataServer.Add(new SrnprCommon.CommonConfig.DataServerEntityCCC() { ServerId = "SO", ServerType = "MSSQL", ServerConn = "" });


            return ce;

        }



    }
}
