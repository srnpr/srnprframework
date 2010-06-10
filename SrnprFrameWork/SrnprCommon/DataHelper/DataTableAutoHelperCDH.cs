using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SrnprCommon.DataHelper
{
    class DataTableAutoHelperCDH
    {



        public DataTable GetDataTable(ReplaceFile.DataServerEntityCRF dse, string sql, SqlParameter[] sp)
        {
            DataTable dt;
            switch (dse.ServerType)
            {
                case SrnprCommon.EnumCommon.DataServerType.MsSql:
                    dt = DataHelper.SqlHelperCDH.ExecuteDataTable(dse.ConnString, sql, sp);
                    break;

                default:
                    dt = new DataTable();
                    break;
            }
            return dt;




        }



    }
}
