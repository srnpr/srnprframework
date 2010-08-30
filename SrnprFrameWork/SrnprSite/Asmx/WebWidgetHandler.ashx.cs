using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace SrnprSite.Asmx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    
    public class WebWidgetHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string s = context.Request["json"];




            SrnprWeb.WebEntity.WidgetRequestWWE req = SrnprWeb.WebProcess.WidgetProcessWWP.DeserializeRequest(s);

            string sRes = SrnprWeb.WebProcess.WidgetProcessWWP.Response(req, DicProcess(req));

            context.Response.Write(sRes);
        }




        /// <summary>
        /// 
        /// Description: 返回附加的数据
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:38:44
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> DicProcess(SrnprWeb.WebEntity.WidgetRequestWWE req)
        {

            Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> dic = new Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE>();



            for (int i = 0, j = req.RQ.Count; i < j; i++)
            {

                switch (req.RQ[i].WidgetType)
                {

                    case "LS":

                        DataTable dtls = GetLSTableById(req.RQ[i].Id);

                        if (dtls != null)
                        {
                            dic[i] = new SrnprWeb.WebEntity.WidgetProcessWWE();
                            dic[i].DataInfo = dtls;
                        }

                        break;
                    case "GS":

                        DataTable dtgs = GetGSTableById(req.RQ[i].Id);

                        if (dtgs != null&&dtgs.Columns.Count>0)
                        {
                            dic[i] = new SrnprWeb.WebEntity.WidgetProcessWWE();
                            dic[i].DataInfo = dtgs;
                            dic[i].DataFlag = true;
                        }

                        break;
                }



            }
            return dic;
        }




        public DataTable GetLSTableById(string sId)
        {

            DataTable dt = new DataTable();

            switch (sId)
            {
                case "abc":


                    List<SrnprWeb.WebEntity.ItemKvdWWE> kvd = new List<SrnprWeb.WebEntity.ItemKvdWWE>();
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk2", V = "vvv2", D = "ddd2" });

                    dt = SrnprCommon.CommonFunction.ListStaticCCF.ListToDataTable<SrnprWeb.WebEntity.ItemKvdWWE>(kvd);


                    break;
                case "":

                    break;
            }

            return dt;

        }








        /// <summary>
        /// 
        /// Description: GridShow加载数据
        /// Author:Liudpc
        /// Create Date: 2010-8-30 14:01:58
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        public DataTable GetGSTableById(string sId)
        {

            DataTable dt = new DataTable();

            switch (sId)
            {
                case "test.gs.listkvd":

                    dt.TableName = "test.gs.listkvd";

                    List<SrnprWeb.WebEntity.ItemKvdWWE> kvd = new List<SrnprWeb.WebEntity.ItemKvdWWE>();
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });

                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });

                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk1", V = "vvv1", D = "ddd1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { K = "kkk2", V = "vvv2", D = "ddd2" });

                    dt = SrnprCommon.CommonFunction.ListStaticCCF.ListToDataTable<SrnprWeb.WebEntity.ItemKvdWWE>(kvd);


                    break;
                case "":

                    break;
            }

            return dt;

        }










        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        

    }
}
